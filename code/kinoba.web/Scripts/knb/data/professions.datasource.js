var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var KinobaData;
(function (KinobaData) {
    var ProfessionsDataSource = (function (_super) {
        __extends(ProfessionsDataSource, _super);
        function ProfessionsDataSource() {
            _super.call(this);

            _super.prototype.init.call(this, {
                data: professionsData
            });
        }
        return ProfessionsDataSource;
    })(kendo.data.DataSource);
    KinobaData.ProfessionsDataSource = ProfessionsDataSource;
})(KinobaData || (KinobaData = {}));
//# sourceMappingURL=professions.datasource.js.map
