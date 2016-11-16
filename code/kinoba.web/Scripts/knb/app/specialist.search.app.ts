
var layout: kendo.Layout;

// Models
var searchParamsModel: KinobaModels.SpecialistSearchParamsModel;
var searchResultsModel: KinobaModels.SpecialistSearchResultsModel;

// Views
var specSearchFormView: kendo.View;
var specSearchResultsView: kendo.View;

var router: kendo.Router;
var scrollPhotosInterval = null;
var scrolledElement = null;

var stickyFormTop: number;

function initApp() {
    

    layout = new kendo.Layout("layout-template");

    router = new kendo.Router({
        init: () => {
            layout.render("#application");
        },
        routeMissing: e => {
            console.log(e);
                        
            router.navigate("/");
        }
    });

    router.route('/', () => {
        searchParamsModel = new KinobaModels.SpecialistSearchParamsModel();
        searchParamsModel.onSearchDataReceived(searchDataReceived);
        specSearchFormView = new kendo.View('#search-form-template', { model: searchParamsModel });
        specSearchFormView.render('#searchFormView');
        

        searchResultsModel = new KinobaModels.SpecialistSearchResultsModel();
        specSearchResultsView = new kendo.View('#search-results-template', { model: searchResultsModel });
        specSearchResultsView.render('#searchResultsView');

        stickyFormTop = $('#searchFormView').offset().top;

    });


    $(window).scroll(function () {
        if ($(window).scrollTop() > stickyFormTop) {
            $('#searchFormView').addClass('sticked');
            $('#stickyalias').css('display', 'block');
        } else {
            $('#searchFormView').removeClass('sticked');
            $('#stickyalias').height($('#searchFormView').height() + 30);
            $('#stickyalias').css('display', 'none');
        }
    });
}

function searchDataReceived(response: KinobaData.IDataResult) {
    searchResultsModel.set('dataResult', response);
    searchResultsModel.set('showResultHeader', true);
}

function startScrollItems(elem: any) {
    if (scrollPhotosInterval != null)
        window.clearInterval(scrollPhotosInterval);

    scrolledElement = elem;
    scrollPhotosInterval = window.setInterval(scrollPhotos, 1200);
}

function stopScrollItems() {
    if (scrollPhotosInterval != null)
        window.clearInterval(scrollPhotosInterval);
}

function scrollPhotos() {
    if (scrolledElement == null)
        return;

    var count = $(scrolledElement).find('li').length;
    var si = $(scrolledElement).data('scrollindex');
    if (si == null)
        si = 0;

    si++;

    if (si >= count)
        si = 0;
        
    $(scrolledElement).data('scrollindex', si);

    var container: any = $(scrolledElement);
    container.scrollTo($($(scrolledElement).find('li')[si]), 300);
}

function showField(val: any) {
    if (val == null)
        return '';

    return val;
}

$(document).on('click', '.k-overlay', function () {
    var kendoWindow = $('.k-window-content.k-content', $(this).next('div.k-widget.k-window'));
    if (kendoWindow == null || kendoWindow.length == 0) {
        return;
    }
    kendoWindow.data('kendoWindow').close();
});

$(document).ready((e) => {
    initApp();
    router.start();

    
});

