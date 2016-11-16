var KinobaCore;
(function (KinobaCore) {
    var School = (function () {
        function School(name, id) {
            if (typeof id === "undefined") { id = null; }
            this.id = id;
            this.name = name;
        }
        return School;
    })();
    KinobaCore.School = School;
})(KinobaCore || (KinobaCore = {}));
//# sourceMappingURL=school.js.map
