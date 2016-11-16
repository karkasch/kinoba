var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var KinobaModels;
(function (KinobaModels) {
    var SpecialistSearchResultsModel = (function (_super) {
        __extends(SpecialistSearchResultsModel, _super);
        function SpecialistSearchResultsModel() {
            _super.call(this);

            this.dataResult = null;
        }
        SpecialistSearchResultsModel.prototype.specSelected = function (e) {
            //console.log(e);
            var specModel = new KinobaModels.SpecialistDetailsModel();

            kendo.unbind('#specDetailsWindow');

            specModel.set('specialist', e.data);
            specModel.set('isVisible', true);

            kendo.bind('#specDetailsWindow', specModel, '');
            var wnd = $('#specDetailsWindow').data('kendoWindow');
            wnd.title('' + e.data.firstName + ' ' + e.data.lastName);

            //wnd.setOptions({ open: { duration: 100 } });
            var w = $(window).width() - 100;
            var h = $(window).width() - 50;

            wnd.setOptions({
                width: w,
                height: 620
            });
            wnd.center().open();
        };

        SpecialistSearchResultsModel.prototype.resultsVisible = function () {
            //console.log('resultsVisible: this.dataResult in ', this.dataResult);
            //return this.dataResult != null;
            return false;
        };

        SpecialistSearchResultsModel.prototype.onPhotosOver = function (e) {
            startScrollItems(e.currentTarget);
        };

        SpecialistSearchResultsModel.prototype.onPhotosOut = function (e) {
            stopScrollItems();
        };
        return SpecialistSearchResultsModel;
    })(kendo.data.ObservableObject);
    KinobaModels.SpecialistSearchResultsModel = SpecialistSearchResultsModel;
})(KinobaModels || (KinobaModels = {}));
//# sourceMappingURL=specialist.search.results.model.js.map
