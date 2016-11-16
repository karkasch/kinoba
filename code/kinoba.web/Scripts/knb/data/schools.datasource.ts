module KinobaData {
    export class SchoolsDataSource extends kendo.data.DataSource {

        constructor() {
            super();

            super.init({
                data: schoolsData
            });
        }
    }
} 