module KinobaData {
    export class ProfessionsDataSource extends kendo.data.DataSource {
        constructor() {
            super();

            super.init({
                data: professionsData
            });
        }
    }
}