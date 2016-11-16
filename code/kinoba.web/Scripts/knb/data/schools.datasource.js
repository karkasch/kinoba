var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var KinobaData;
(function (KinobaData) {
    var SchoolsDataSource = (function (_super) {
        __extends(SchoolsDataSource, _super);
        function SchoolsDataSource() {
            _super.call(this);

            _super.prototype.init.call(this, {
                data: schoolsData
            });
        }
        return SchoolsDataSource;
    })(kendo.data.DataSource);
    KinobaData.SchoolsDataSource = SchoolsDataSource;
})(KinobaData || (KinobaData = {}));
//# sourceMappingURL=schools.datasource.js.map
