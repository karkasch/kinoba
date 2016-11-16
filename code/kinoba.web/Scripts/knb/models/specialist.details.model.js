var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var KinobaModels;
(function (KinobaModels) {
    var SpecialistDetailsModel = (function (_super) {
        __extends(SpecialistDetailsModel, _super);
        function SpecialistDetailsModel() {
            _super.call(this);
            this.windowTitle = 'Специалист';
        }
        SpecialistDetailsModel.prototype.sexIcon = function () {
            //console.log('sexIcon', this.specialist);
            if (this.specialist.sex == 1)
                return "M";
            else if (this.specialist.sex == 2)
                return "Ж";

            return "";
        };
        return SpecialistDetailsModel;
    })(kendo.data.ObservableObject);
    KinobaModels.SpecialistDetailsModel = SpecialistDetailsModel;
})(KinobaModels || (KinobaModels = {}));
//# sourceMappingURL=specialist.details.model.js.map
