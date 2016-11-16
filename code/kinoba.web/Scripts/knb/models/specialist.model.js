var __extends = this.__extends || function (d, b) {
    for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p];
    function __() { this.constructor = d; }
    __.prototype = b.prototype;
    d.prototype = new __();
};
var KinobaModels;
(function (KinobaModels) {
    var SpecialistModel = (function (_super) {
        __extends(SpecialistModel, _super);
        function SpecialistModel() {
            _super.call(this);

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
        SpecialistModel.prototype.save = function () {
            var _this = this;
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

            var method = 'POST';

            //var id = this.id == null ? '' : '' + this.id;
            $.ajax({
                type: method,
                data: JSON.stringify(specialist),
                url: '/api/specialist/',
                //headers: {
                //    Accept: "application/json; charset=utf-8"
                //},
                contentType: 'application/json'
            }).done(function (response) {
                if (response.id > 0 && (_this.get('id') == null || _this.get('id') == '')) {
                    //$('#photosUpload').data('url', '/api/photoimages/' + response.id);
                    initUploader(response.id);
                    _this.updateView();
                }

                if (response.id > 0) {
                    _this.set('id', response.id);
                    _this.set('slug', response.slug);
                    _this.set('showUpload', true);

                    appNotification.show('Данные сохранены', 'string');
                }

                _this.set('showProgress', false);

                console.log(' done', response);
            }).fail(function (response) {
                console.log('fail', response);
                _this.set('showProgress', false);
            });
        };

        SpecialistModel.prototype.getInt = function (val) {
            if (val == null)
                return 0;

            return parseInt(val);
        };

        SpecialistModel.prototype.getListIds = function (items) {
            var result = new Array();
            for (var i = 0; i < items.length; i++)
                result.push(parseInt(items[i]));

            return result;
        };

        SpecialistModel.prototype.updateView = function () {
            this.set('showUpload', (this.id > 0));

            if (this.id > 0)
                $('#photosUpload').attr('data-url', '/api/photoimages/' + specEditModel.id);
        };

        SpecialistModel.prototype.addUploadedPhoto = function (item) {
            this.photos.push(item);
            //var medias = this.medias;
            //medias.push(item);
            //this.set('medias', medias);
        };

        SpecialistModel.prototype.deletePhotoLink = function (e) {
            var id = $(e.currentTarget).data('id');

            for (var i = 0; i < this.photos.length; i++) {
                if (this.photos[i].id == id) {
                    this.photos.splice(i, 1);
                }
            }
        };

        SpecialistModel.prototype.addVideoLink = function () {
            var specialistMedia = KinobaModels.SpecialistMedia.parseLink(this.videoLink);

            this.medias.push(specialistMedia);

            //var medias = this.medias;
            //medias.push(specialistMedia);
            //this.set('medias', medias);
            this.set('videoLink', null);
        };

        SpecialistModel.prototype.deleteVideoLink = function (e) {
            var link = $(e.currentTarget).data('link');

            for (var i = 0; i < this.medias.length; i++) {
                if (this.medias[i].link == link) {
                    this.medias.splice(i, 1);
                }
            }
        };
        return SpecialistModel;
    })(kendo.data.ObservableObject);
    KinobaModels.SpecialistModel = SpecialistModel;

    var SpecialistPhoto = (function () {
        function SpecialistPhoto() {
        }
        return SpecialistPhoto;
    })();
    KinobaModels.SpecialistPhoto = SpecialistPhoto;

    var SpecialistMedia = (function () {
        function SpecialistMedia(id, mediaType, link, embedCode) {
            this.id = id;
            this.mediaType = mediaType;
            this.link = link;
            this.embedCode = embedCode;
        }
        SpecialistMedia.parseLink = function (link) {
            if (link.indexOf('youtube.com') >= 0 || link.indexOf('youtu.be') >= 0) {
                var re1 = /^.+watch\?v=([a-zA-Z0-9_\-]+).*$/;
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
        };
        return SpecialistMedia;
    })();
    KinobaModels.SpecialistMedia = SpecialistMedia;
})(KinobaModels || (KinobaModels = {}));
//# sourceMappingURL=specialist.model.js.map
