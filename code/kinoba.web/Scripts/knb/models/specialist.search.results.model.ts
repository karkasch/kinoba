module KinobaModels {
    export class SpecialistSearchResultsModel extends kendo.data.ObservableObject {

        dataResult: KinobaData.SpecialistSearchResult;
        showResultHeader: boolean;

        constructor() {
            super();

            this.dataResult = null;
        }

        specSelected(e: any) {
            //console.log(e);

            var specModel = new KinobaModels.SpecialistDetailsModel();

            kendo.unbind('#specDetailsWindow');

            specModel.set('specialist', e.data);
            specModel.set('isVisible', true);

            kendo.bind('#specDetailsWindow', specModel, '');
            var wnd: kendo.ui.Window = $('#specDetailsWindow').data('kendoWindow');
            wnd.title('' + e.data.firstName + ' ' + e.data.lastName);

            //wnd.setOptions({ open: { duration: 100 } });

            var w = $(window).width() - 100;
            var h = $(window).width() - 50;

            wnd.setOptions({
                width: w,
                height: 620,
                //animation: {
                //    open: 800,
                //    close: 300
                //},
            });
            wnd.center().open();
        }

        resultsVisible() {
            //console.log('resultsVisible: this.dataResult in ', this.dataResult);
            //return this.dataResult != null;
            return false;
        }

        onPhotosOver(e: JQueryEventObject) {
            startScrollItems(e.currentTarget);
        }

        onPhotosOut(e: any) {
            stopScrollItems();
        }
    }
} 