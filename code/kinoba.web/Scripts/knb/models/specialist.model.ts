declare var citiesData: Array<any>;
declare var eyeColorsData: Array<any>;
declare var hairTypesData: Array<any>;


module KinobaModels {
    export class SpecialistModel extends kendo.data.ObservableObject {
        id: number;
        slug: string;

        professionsDataSource: KinobaData.ProfessionsDataSource;
        schoolsDataSource: KinobaData.SchoolsDataSource;
        citiesDataSource: kendo.data.DataSource;
        eyeColorsDataSource: kendo.data.DataSource;
        hairTypesDataSource: kendo.data.DataSource;

        professionIds: Array<number>;
        sex: number;
        schoolIds: Array<number>
        birthDate: Date;
        height: number;
        clothesSize: number;
        shoeSize: number;
        eyeColorId: number;
        hairColor: number;
        hairTypeId: number;
        cityId: number;
        firstName: string;
        lastName: string;
        phone: string;
        email: string;
        passport: string;
        castingDate: Date;
        notes: string;

        medias: Array<KinobaModels.SpecialistMedia>;
        photos: Array<KinobaModels.SpecialistPhoto>;
        videoLink: string;

        showProgress: boolean;
        showUpload: boolean;
        uploadUrl: string;

        constructor() {
            super();

            //this.set('sex', 1);
            this.medias = [];
            this.photos = [];

            //this.professionsDataSource = new Kinoba.Data.ProfessionsDataSource();
            //this.schoolsDataSource = new KinobaData.SchoolsDataSource();
            //this.citiesDataSource = new kendo.data.DataSource({ data: citiesData });
            //this.eyeColorsDataSource = new kendo.data.DataSource({ data: eyeColorsData });
            //this.hairTypesDataSource = new kendo.data.DataSource({ data: hairTypesData });

            //this.bind('change', (e) => {
            //    if (e.field == 'id')
            //        $('#photosUpload').data('url', '/api/photoimages/' + this.id);
            //});

            //this.set('showProgress', true);
        }

        save() {

            console.log(this);

            var specialist = {
                id: this.id,
                slug: this.slug,
                professionIds: this.getListIds(this.professionIds),
                sex: this.getInt(this.sex),
                schoolIds: this.getListIds(this.schoolIds),
                birthDate: this.birthDate,
                height: this.height,
                clothesSize: this.clothesSize,
                shoeSize: this.shoeSize,
                eyeColor: this.getInt(this.eyeColorId),
                hairLength: this.getInt(this.hairTypeId),
                hairColor: this.getInt(this.hairColor),
                cityId: this.getInt(this.cityId),
                firstName: this.firstName,
                lastName: this.lastName,
                phone: this.phone,
                email: this.email,
                passport: this.passport,
                castingDate: this.castingDate,
                notes: this.notes,

                specialistPhotos: this.photos,
                specialistMedias: this.medias 
            };


            this.set('showProgress', true);

            var method = 'POST'; // this.id != null && this.id > 0 ? 'PUT' : 'POST';
            //var id = this.id == null ? '' : '' + this.id;


            $.ajax({
                type: method,
                data: JSON.stringify(specialist),
                url: '/api/specialist/',
                //headers: {
                //    Accept: "application/json; charset=utf-8"
                //},
                contentType: 'application/json'
                //dataType: 'json',
                //accepts: 'json'
                //success: (responseData, textStatus) => {
                //    console.log("File uploaded successfully", responseData);
                //},
                //error: (jqXHR, textStatus, errorThrown) => {
                //    console.log('errrr', errorThrown)
                //}
            }).done((response: any) => {
                if (response.id > 0 && (this.get('id') == null || this.get('id') == '')) {
                    //$('#photosUpload').data('url', '/api/photoimages/' + response.id);
                    initUploader(response.id);
                    this.updateView();
                }

                if (response.id > 0) {
                    this.set('id', response.id);
                    this.set('slug', response.slug);
                    this.set('showUpload', true);

                    appNotification.show('Данные сохранены', 'string');
                }

                this.set('showProgress', false);

                console.log(' done', response);
            }).fail((response: any) => {
                console.log('fail', response);
                this.set('showProgress', false);
            });


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

        updateView() {
            this.set('showUpload', (this.id > 0));

            if (this.id > 0)
                $('#photosUpload').attr('data-url', '/api/photoimages/' + specEditModel.id);
        }

        addUploadedPhoto(item: SpecialistPhoto) {
            this.photos.push(item);

            //var medias = this.medias;
            //medias.push(item);
            //this.set('medias', medias);
        }

        deletePhotoLink(e: any) {
            var id = $(e.currentTarget).data('id');

            for (var i = 0; i < this.photos.length; i++) {
                if (this.photos[i].id == id) {
                    this.photos.splice(i, 1);
                }
            }
        }

        addVideoLink() {
            var specialistMedia = KinobaModels.SpecialistMedia.parseLink(this.videoLink);

            this.medias.push(specialistMedia);

            //var medias = this.medias;
            //medias.push(specialistMedia);
            //this.set('medias', medias);

            this.set('videoLink', null);
        }

        deleteVideoLink(e: any) {
            var link = $(e.currentTarget).data('link');

            for (var i = 0; i < this.medias.length; i++) {
                if (this.medias[i].link == link) {
                    this.medias.splice(i, 1);
                }
            }
        }
    }

    export class SpecialistPhoto {
        id: number;
        url: string;
        fileName: string;

    }

    export class SpecialistMedia {
        id: number;
        mediaType: number;
        link: string;
        embedCode: string;

        constructor(id: number, mediaType: number, link: string, embedCode: string) {
            this.id = id;
            this.mediaType = mediaType;
            this.link = link;
            this.embedCode = embedCode;
        }

        static parseLink(link: string) : SpecialistMedia {
            if (link.indexOf('youtube.com') >= 0 || link.indexOf('youtu.be') >= 0) {
                var re1 = /^.+watch\?v=([a-zA-Z0-9_\-]+).*$/; // new RegExp('.+watch\?v=([a-zA-Z0-9_-]+).*');
                var res = re1.exec(link);
                if (res != null && res.length > 1) {
                    var code = '<iframe width="560" height="315" src="https://www.youtube.com/embed/' + res[1] + '?rel=0" frameborder="0" allowfullscreen></iframe>';
                    return new SpecialistMedia(null, 2, link, code);
                }

                var re2 = /^.+\/([a-zA-Z0-9_\-]+).*$/;
                res = re2.exec(link);
                if (res != null && res.length > 1) {
                    var code = '<iframe width="560" height="315" src="https://www.youtube.com/embed/' + res[1] + '?rel=0" frameborder="0" allowfullscreen></iframe>';
                    return new SpecialistMedia(null, 2, link, code);
                }
            }

            return new SpecialistMedia(null, 0, link, null);
        }

        
    }
        
} 