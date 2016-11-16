module KinobaModels {
    class Search {
        pageNum: number;
        pageSize: number;

        cityId: number;
        professionIds: Array<number>;
        sex: number;
        ageFrom: number;
        ageTo: number;
        firstName: string;
        lastName: string;
    }

    export class SpecialistSearchParamsModel extends kendo.data.ObservableObject {
        selectedCity: any;
        professionIds: Array<KinobaData.ListDataItem>;
        sex: number;
        ageFrom: number;
        ageTo: number;
        firstName: string;
        lastName: string;

        showProgress: boolean;

        searchDataReceivedHandler: Function;

        citiesDataSource: kendo.data.DataSource;
        professionsDataSource: kendo.data.DataSource;

        //
        summaryText: string;
        summaryVisible: boolean;
        formVisible: boolean;

        constructor() {
            super();

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

        search() {

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
            }).done((response: KinobaData.IDataResult) => {
                console.log('search done', response);
                this.setSummaryText();
                this.toggleView(false);
                this.set('showProgress', false);
                this.searchDataReceivedHandler(response);
            }).fail((response: any) => {
                console.log('search fail', response);
                this.set('showProgress', false);
                alert(response);
            });
        }

        onSummaryClick() {
            this.toggleView(true);
        }

        toggleView(showForm: boolean) {
            if (showForm) {
                this.set('summaryVisible', false);
                this.set('formVisible', true);
            }
            else {
                this.set('summaryVisible', true);
                this.set('formVisible', false);
            }
        }

        setSummaryText() {
            var txt = '';

            if (this.selectedCity != null && this.selectedCity.id > 0) {
                //txt += 'Город: ';
                txt += this.selectedCity.name;
                txt += '&nbsp;&nbsp;';
            }

            if (this.professionIds != null && this.professionIds.length > 0) {
                //txt += '&nbsp;&nbsp;&nbsp;Профессия: ';
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
        }

        getInt(val: any): number {
            if (val == null)
                return 0;

            

            return parseInt(val);
        }

        getListIds(items: Array<any>): Array<number> {
            var result: Array<number> = new Array<number>();
            for (var i = 0; i < items.length; i++)
                result.push(parseInt(items[i]));

            return result;
        }

        getModelListIds(items: Array<KinobaData.ListDataItem>): Array<number> {
            var result: Array<number> = new Array<number>();
            for (var i = 0; i < items.length; i++)
                result.push(items[i].id);

            return result;
        }

        onSearchDataReceived(handler: Function) {
            this.searchDataReceivedHandler = handler;
        }
    }
} 