var hostPath = 'http://localhost:48893/api/SearchUrls';
function SearchUrls(model) {
    var dataModel = model;
    function onSearchTypeChanged(s) {
       
        var selected = ko.utils.arrayFilter(s.searchUrlsModel.Urls(), function (i) {
            return i.SearchType() == s.selectedSearchType();
        });
        var value = '';
        for (var i = 0; i < selected.length; i++) {
            value += selected[i].Url();
            value += '\n';
        }
        s.selectedSearchUrls(value);
     
    }
    function saveSearchUrls(s) {
        var items = vm.selectedSearchUrls().split('\n');
        var urls = [];
        var notSelected = ko.utils.arrayFilter(s.searchUrlsModel.Urls(), function(url) {
            return url.SearchType() != s.selectedSearchType();
        });
        for (var i = 0; i < notSelected.length; i++) {
            urls.push({ SearchType: notSelected[i].SearchType(), Url: notSelected[i].Url().trim() });
        }
        for (var i = 0; i < items.length; i++) {
            if (items[i]!='') {
                urls.push({ SearchType: s.selectedSearchType(), Url: items[i].trim() });
            };
        }
        dataModel.Urls = urls;
        $.post(hostPath, dataModel, function(r, status) {
            var m = ko.mapping.fromJS(r);
            s.searchUrlsModel.Urls(m.Urls());
        });
 
    }
    var result = {
        availableSearchTypes: ['Craigslist' ],
        selectedSearchType:null,
        onSearchTypeChanged: onSearchTypeChanged,
        searchUrlsModel: model,
        selectedSearchUrls: null,
        saveSearchUrls:saveSearchUrls
    }
    
    return result;
}

var vm = null;
$.get(hostPath, function(s, r) {
    vm = ko.mapping.fromJS(new SearchUrls(s));
    ko.applyBindings(vm);
});
