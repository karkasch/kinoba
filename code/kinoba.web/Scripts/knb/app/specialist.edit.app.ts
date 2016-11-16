declare var professionsData: Array<KinobaData.ListDataItem>;
declare var schoolsData: Array<KinobaData.ListDataItem>;

var layout: kendo.Layout = new kendo.Layout("layout-template");
var appNotification: kendo.ui.Notification;

// Models
var specEditModel: KinobaModels.SpecialistModel = new KinobaModels.SpecialistModel();

// Views
var specEditView: kendo.View = new kendo.View('#specialist-template', { model: specEditModel });


$(document).ready(function () {
    //specEditModel.set('schools', [1, 2]);

    layout.render('#application');
    layout.showIn('#castingForm', specEditView);

    specEditModel.updateView();

    if (specEditModel.id > 0)
        initUploader(specEditModel.id);
        
    //$('#photosUpload').data('url', '/api/photoimages/' + specEditModel.id);
    appNotification = $("#appNotification").kendoNotification({
        stacking: "down",
        button: true
    }).data("kendoNotification");
});

function initUploader(specId: number) {
    var uploader: any = $('#photosUpload');

    //$(uploader).data('url', '/api/photoimages/' + specId);
    $(uploader).attr('data-url', '/api/photoimages/' + specId);

    uploader.fileupload({
        dataType: 'json',
        singleFileUploads: false,
        limitMultiFileUploads: 9,
        done: function (e, data) {
            if (data.result.result != null && data.result.result == false) {
                alert(data.result.msg);
                return;
            }

            $.each(data.result, function (index, file) {
                specEditModel.addUploadedPhoto(file);
            });
        }
    });
}


//function parseVideoLink(link: string): KinobaModels.SpecialistMedia {
//    if (link.indexOf('youtube.com') >= 0 || link.indexOf('youtu.be') >= 0) {
//        var re1 = /^.+watch\?v=([a-zA-Z0-9_\-]+).*$/; // new RegExp('.+watch\?v=([a-zA-Z0-9_-]+).*');
//        var res = re1.exec(link);
//        if (res != null && res.length > 1) {
//            var result = new KinobaModels.SpecialistMedia();
//            result.mediaType = 2;
//            result.linkId = res[1];
//            return result;
//        }

//        var re2 = /^.+\/([a-zA-Z0-9_\-]+).*$/;
//        res = re2.exec(link);
//        if (res != null && res.length > 1)
//            return { mediaType: 2, id: res[1] };

//    }

//    return { mediaType: 0, id: link };
//}

//function generateEmbedCode(link: any) {
//    if (link.source == 'youtube') {
//        return '';
//    }
//}


