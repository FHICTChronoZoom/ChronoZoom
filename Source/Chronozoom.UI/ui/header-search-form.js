/// <reference path='../ui/controls/formbase.ts'/>
/// <reference path='../scripts/search.ts'/>
/// <reference path='../scripts/typings/jquery/jquery.d.ts'/>
var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var CZ;
(function (CZ) {
    (function (UI) {
        var FormHeaderSearch = (function (_super) {
            __extends(FormHeaderSearch, _super);
            function FormHeaderSearch(container, formInfo) {
                _super.call(this, container, formInfo);

                this.searchToDate = new CZ.UI.DatePicker(container.find(formInfo.searchToDate), true);
                this.searchFromDate = new CZ.UI.DatePicker(container.find(formInfo.searchFromDate), true);
                this.searchTextbox = container.find(formInfo.searchTextbox);
                this.searchScope = $('#scope');
                this.searchResultsBox = container.find(formInfo.searchResultsBox);
                this.progressBar = container.find(formInfo.progressBar);
                this.resultSections = container.find(formInfo.resultSections);
                this.resultsCountTextblock = container.find(formInfo.resultsCountTextblock);

                this.initialize();
            }
            FormHeaderSearch.prototype.initialize = function () {
                var _this = this;
                this.fillFormWithSearchResults();

                this.searchResults = [];
                this.progressBar.css("opacity", 0);
                this.hideResultsCount();
                this.clearResultSections();
                this.hideSearchResults();
                this.searchTextbox.off();
                this.searchToDate.editModeYear();
                this.searchFromDate.editModeYear();

                // populate search scope option choices
                CZ.Service.getSearchScopeOptions().done(function (response)
                {
                    _this.searchScope.find('select').html('');

                    for (loop = 0; loop < response.length; loop++)
                    {
                        _this.searchScope.find('select').append(new Option
                        (
                            response[loop].Value,
                            response[loop].Key,
                            response[loop].Key == 1,
                            response[loop].Key == 1
                        ));
                    }
                });

                var onSearchQueryChanged = function (event)
                {
                    if (_this.searchTextbox.val().length < 2) return;

                    clearTimeout(_this.delayedSearchRequest);

                    _this.delayedSearchRequest = setTimeout(function ()
                    {
                        var query = _this.searchTextbox.val();
                        var scope = parseInt(_this.searchScope.find('select').val());
                        var fromdate = _this.searchFromDate.getDate();
                        var todate = _this.searchToDate.getDate();
                        query     = _this.escapeSearchQuery(query);
                        _this.showProgressBar();
                        _this.sendSearchQuery(query, scope, fromdate, todate).then(function (response)
                        {
                            _this.hideProgressBar();
                            _this.searchResults = response;
                            _this.updateSearchResults();
                        }, function (error)
                        {
                            console.log("Error connecting to service: search.\n" + error.responseText);
                        });
                    }, 300);
                };

                this.searchTextbox.on('input search', onSearchQueryChanged);
                this.searchTextbox.on('input search', onSearchQueryChanged);
                //this.searchFromDate.on('input search', onSearchQueryChanged);
                //this.searchToDate.on('input search', onSearchQueryChanged);
                this.searchScope.find('select').on('change', onSearchQueryChanged);

                // NOTE: Workaround for IE9. IE9 doesn't fire 'input' event on backspace/delete buttons.
                //       http://www.useragentman.com/blog/2011/05/12/fixing-oninput-in-ie9-using-html5widgets/
                //       https://github.com/zoltan-dulac/html5Forms.js/blob/master/shared/js/html5Widgets.js
                var isIE9 = (CZ.Settings.ie === 9);
                if (isIE9) {
                    this.searchTextbox.on("keyup", function (event) {
                        switch (event.which) {
                            case 8:
                            case 46:
                                onSearchQueryChanged(event);
                                break;
                        }
                    });
                    this.searchTextbox.on("cut", onSearchQueryChanged);
                }
            };


            FormHeaderSearch.prototype.sendSearchQuery = function (query, scope, fromdate, todate)
            {
                return (query === "") ? $.Deferred().resolve(null).promise() : CZ.Service.getSearch(query, scope, fromdate, todate);
            };

            FormHeaderSearch.prototype.updateSearchResults = function () {
                var _this = this;
                this.clearResultSections();

                // Query string is empty. No update.
                if (this.searchResults === null) {
                    this.hideSearchResults();
                    return;
                }

                // No results for this query.
                if (this.searchResults.length === 0) {
                    this.showNoResults();
                    return;
                }

                // There are search results. Show them.
                var resultTypes = {
                    0: "exhibit",
                    1: "timeline",
                    2: "contentItem"
                };

                var sections = {
                    exhibit: $(this.resultSections[1]),
                    timeline: $(this.resultSections[0]),
                    contentItem: $(this.resultSections[2])
                };

                var idPrefixes = {
                    exhibit: "e",
                    timeline: "t",
                    contentItem: ""
                };

                this.searchResults.forEach(function (item)
                {
                    var form        = _this;
                    var resultType  = resultTypes[item.ObjectType];
                    var resultId    = idPrefixes[resultType] + item.Id;
                    var resultTitle = item.Title;
                    var resultURL   = item.ReplacementURL ? item.ReplacementURL : '';

                    sections[resultType].append($('<div></div>',
                    {
                        class:              'cz-form-search-result',
                        title:              'Curator: "' + item.UserName + '". Collection: "' + item.CollectionName + '".',
                        text:               resultTitle,
                        'data-result-id':   resultId,
                        'data-result-type': resultType,
                        'data-result-url':  resultURL,
                        click: function ()
                        {
                            var self = $(this);

                            if (self.attr('data-result-url') == '')
                            {
                                CZ.Search.goToSearchResult(self.attr('data-result-id'), self.attr('data-result-type'));
                                form.close();
                            }
                            else
                            {
                                window.location = self.attr('data-result-url');
                            }
                        }
                    }));
                });

                this.showResultsCount();
                this.showNonEmptySections();
            };

            FormHeaderSearch.prototype.fillFormWithSearchResults = function () {
                // NOTE: Initially the form is hidden. Show it to compute heights and then hide again.
                this.container.show();
                this.searchResultsBox.css("height", "calc(100% - 190px)");
                this.searchResultsBox.css("height", "-moz-calc(100% - 190px)");
                this.searchResultsBox.css("height", "-webkit-calc(100% - 190px)");
                this.searchResultsBox.css("height", "-o-calc(100% - 190px)");
                this.container.hide();
            };

            FormHeaderSearch.prototype.clearResultSections = function () {
                this.resultSections.find("div").remove();
            };

            FormHeaderSearch.prototype.escapeSearchQuery = function (query) {
                return query ? query.replace(/"/g, "") : "";
            };

            FormHeaderSearch.prototype.getResultsCountString = function () {
                var count = this.searchResults.length;
                return count + ((count === 1) ? " Result:" : " Results:");
            };

            FormHeaderSearch.prototype.showProgressBar = function () {
                this.progressBar.animate({
                    opacity: 1
                });
            };

            FormHeaderSearch.prototype.hideProgressBar = function () {
                this.progressBar.animate({
                    opacity: 0
                });
            };

            FormHeaderSearch.prototype.showNonEmptySections = function () {
                this.searchResultsBox.show();
                this.resultSections.show();
                this.resultSections.each(function (i, item) {
                    var results = $(item).find("div");
                    if (results.length === 0) {
                        $(item).hide();
                    }
                });
            };

            FormHeaderSearch.prototype.showNoResults = function () {
                this.searchResultsBox.show();
                this.resultSections.hide();
                this.resultsCountTextblock.show();
                this.resultsCountTextblock.text("No results");
            };

            FormHeaderSearch.prototype.showResultsCount = function () {
                this.searchResultsBox.show();
                this.resultsCountTextblock.show();
                this.resultsCountTextblock.text(this.getResultsCountString());
            };

            FormHeaderSearch.prototype.hideResultsCount = function () {
                this.resultsCountTextblock.hide();
            };

            FormHeaderSearch.prototype.hideSearchResults = function () {
                this.hideResultsCount();
                this.searchResultsBox.hide();
            };

            FormHeaderSearch.prototype.show = function () {
                var _this = this;
                _super.prototype.show.call(this, {
                    effect: "slide",
                    direction: "left",
                    duration: 500,
                    complete: function () {
                        _this.searchTextbox.focus();
                    }
                });

                this.activationSource.addClass("active");
            };

            FormHeaderSearch.prototype.close = function () {
                _super.prototype.close.call(this, {
                    effect: "slide",
                    direction: "left",
                    duration: 500
                });

                this.activationSource.removeClass("active");
            };
            return FormHeaderSearch;
        })(CZ.UI.FormBase);
        UI.FormHeaderSearch = FormHeaderSearch;
    })(CZ.UI || (CZ.UI = {}));
    var UI = CZ.UI;
})(CZ || (CZ = {}));
