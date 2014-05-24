var hostPath = 'http://localhost:48893/api/Search';
var Settings = function (model) {
    var dataModel = model;
    function addSearch(s) {
  
        s.newSearch = { Name: ''};
        s.showNewSearch(true);
        vm.showAddSearch(canAddSearch());
    }
    function canAddSearch() {
        return ko.utils.arrayFilter(vm.searchSettingsModel.Searches(), function(e) {
            return e.editing() == true;
        }).length == 0 && vm.newSearch==null;
    }
    function removeSearchTerm(search, term) {
        search.SearchTerms.remove(term);

    }
    function deleteSearch(s) {
        var itemIndex = vm.searchSettingsModel.Searches.indexOf(s);
        var newModel = [];
        for (var i = 0; i < dataModel.Searches.length; i++) {
            if (i != itemIndex)
                newModel.push(dataModel.Searches[i]);
        }
        dataModel.Searches = newModel;
        $.post(hostPath, dataModel, function(r, status) {
            vm.searchSettingsModel.Searches.remove(s);
        });
       

    }
    function SearchModel(s) {
        var result = {
            Name: s.Name,
            editing:false,
            SearchTerms: s.SearchTerms?s.SearchTerms:[]
        };
        return ko.mapping.fromJS(result);
    }
    function cancelEditSearch(s) {
        var itemIndex = vm.searchSettingsModel.Searches.indexOf(s);
        var sm = new SearchModel(dataModel.Searches[itemIndex]);
        s.SearchTerms(sm.SearchTerms());
        s.Name(sm.Name());
        s.editing(false);
        //shows the add new search button
        vm.showAddSearch(canAddSearch());
    }
    function saveSearch(s) {
        var itemIndex = vm.searchSettingsModel.Searches.indexOf(s);
        var item = dataModel.Searches[itemIndex];
        item.Name = s.Name();
        item.SearchTerms = s.SearchTerms();
        $.post(hostPath, dataModel, function (r, status) {
            s.editing(false);
            //shows the add new search button
            vm.newSearch = null;
            vm.showAddSearch(canAddSearch());
            
        });
    }
    function cancelAddSearch(s) {
        s.newSearch = null;
        s.showNewSearch(false);
        //shows the add new search button
        vm.showAddSearch(canAddSearch());
    }
    function editSearch(s) {
        s.editing(true);
        vm.showAddSearch(canAddSearch());
    }
    function addSearchTerm(s) {
        var newItem = ko.mapping.fromJS({ Term: '', Score: null });
        s.SearchTerms.push(newItem);
    }
    function saveNewSearch(s) {
       
        var m = ko.mapping.toJS(s);
        var data = m.searchSettingsModel;
        var newSearch = { Name: s.newSearch.name };
        data.Searches.push(newSearch);
        $.post(hostPath, data, function (r, status) {
            vm.searchSettingsModel.Searches.push(new SearchModel(newSearch));
            dataModel = ko.mapping.toJS(vm.searchSettingsModel);
            vm.newSearch = null;
            s.showNewSearch(false);
            //shows the add new search button
            vm.showAddSearch(canAddSearch());
        });
    }
    var result = {
        searchSettingsModel: model,
        showNewSearch: false,
        addSearch: addSearch,
        cancelAddSearch: cancelAddSearch,
        saveSearch:saveSearch,
        saveNewSearch: saveNewSearch,
        editSearch: editSearch,
        deleteSearch:deleteSearch,
        addSearchTerm: addSearchTerm,
        removeSearchTerm: removeSearchTerm,
        cancelEditSearch: cancelEditSearch,
        showAddSearch:true
       

    };
    for (var i = 0; i < model.Searches.length; i++) {
        
        model.Searches[i].editing = false;


    }

    return result;
}
var vm = null;
function init() {
    $.get(hostPath, function (s, d) {

        var settings = new Settings(s);
        
    
        vm = ko.mapping.fromJS(settings);
        ko.applyBindings(vm);
    });
}

init();