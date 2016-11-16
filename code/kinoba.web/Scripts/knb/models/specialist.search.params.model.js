var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var KinobaModels;
(function (KinobaModels) {
    var Search = (function () {
        function Search() {
        }
        return Search;
    })();

    var SpecialistSearchParamsModel = (function (_super) {
        __extends(SpecialistSearchParamsModel, _super);
        function SpecialistSearchParamsModel() {
            _super.call(this);

            //this.cityId = 0;
            this.sex = 0;
            this.professionIds = [];
            this.ageFrom = 0;
            this.ageTo = 100;
            this.firstName = '';
            this.lastName = '';

            this.summaryText = '';
            this.summaryVisible = false;
            this.formVisible = true;

            this.citiesDataSource = new kendo.data.DataSource({
                data: citiesData
            });
            this.professionsDataSource = new kendo.data.DataSource({
                data: professionsData
            });
        }
        SpecialistSearchParamsModel.prototype.search = function () {
            var _this = this;
            var search = new Search();
            search.pageNum = 1;
            search.pageSize = 50;
            search.cityId = this.selectedCity == null ? 0 : this.selectedCity.id;
            search.ageFrom = this.ageFrom;
            search.ageTo = this.ageTo;
            search.professionIds = this.getModelListIds(this.professionIds);
            search.sex = this.getInt(this.sex);
            search.firstName = this.firstName;
            search.lastName = this.lastName;

            this.set('showProgress', true);

            $.ajax({
                type: 'POST',
                data: JSON.stringify(search),
                url: '/api/SpecialistSearch/',
                contentType: 'application/json'
            }).done(function (response) {
                console.log('search done', response);
                _this.setSummaryText();
                _this.toggleView(false);
                _this.set('showProgress', false);
                _this.searchDataReceivedHandler(response);
            }).fail(function (response) {
                console.log('search fail', response);
                _this.set('showProgress', false);
                alert(response);
            });
        };

        SpecialistSearchParamsModel.prototype.onSummaryClick = function () {
            this.toggleView(true);
        };

        SpecialistSearchParamsModel.prototype.toggleView = function (showForm) {
            if (showForm) {
                this.set('summaryVisible', false);
                this.set('formVisible', true);
            } else {
                this.set('summaryVisible', true);
                this.set('formVisible', false);
            }
        };

        SpecialistSearchParamsModel.prototype.setSummaryText = function () {
            var txt = '';

            if (this.selectedCity != null && this.selectedCity.id > 0) {
                //txt += 'Город: ';
                txt += this.selectedCity.name;
                txt += '&nbsp;&nbsp;';
            }

            if (this.professionIds != null && this.professionIds.length > 0) {
                for (var i = 0; i < this.professionIds.length; i++) {
                    txt += this.professionIds[i].name;
                    if (i + 1 < this.professionIds.length)
                        txt += ', ';
                }
                txt += '&nbsp;&nbsp;';
            }

            if (this.sex > 0) {
                //txt += '&nbsp;&nbsp;&nbsp;Пол: ';
                if (this.sex == 1)
                    txt += 'мужчина';
                if (this.sex == 2)
                    txt += 'женщина';
                txt += '&nbsp;&nbsp;';
            }

            if (this.ageFrom > 0 || (this.ageTo > 0 && this.ageTo < 100)) {
                //txt += '&nbsp;&nbsp;&nbsp;Возраст';
                if (this.ageFrom > 0)
                    txt += 'от: ' + this.ageFrom + ' ';

                if (this.ageTo > 0 && this.ageTo < 100)
                    txt += ' до: ' + this.ageTo + ' ';
                txt += 'лет&nbsp;&nbsp;';
            }

            if (this.firstName != '') {
                txt += this.firstName;
                txt += ' ';
            }

            if (this.lastName != '') {
                txt += this.lastName;
                txt += '&nbsp;&nbsp;';
            }

            if (txt == '')
                txt = 'Нет фильтра';

            this.set('summaryText', txt);
        };

        SpecialistSearchParamsModel.prototype.getInt = function (val) {
            if (val == null)
                return 0;

            return parseInt(val);
        };

        SpecialistSearchParamsModel.prototype.getListIds = function (items) {
            var result = new Array();
            for (var i = 0; i < items.length; i++)
                result.push(parseInt(items[i]));

            return result;
        };

        SpecialistSearchParamsModel.prototype.getModelListIds = function (items) {
            var result = new Array();
            for (var i = 0; i < items.length; i++)
                result.push(items[i].id);

            return result;
        };

        SpecialistSearchParamsModel.prototype.onSearchDataReceived = function (handler) {
            this.searchDataReceivedHandler = handler;
        };
        return SpecialistSearchParamsModel;
    })(kendo.data.ObservableObject);
    KinobaModels.SpecialistSearchParamsModel = SpecialistSearchParamsModel;
})(KinobaModels || (KinobaModels = {}));
//# sourceMappingURL=specialist.search.params.model.js.map
