

var ViewModelsKO = {};

(function() {

    var postViewModel = {
        Id: ko.observable(),
        PostTitle: ko.observable(),
        PostContent: ko.observable(),
        CreationDate: ko.observable(),
        PostCategories: ko.observableArray(),
        PostComments: ko.observableArray()
    }


    var blogViewModel = {
        blogTitle: ko.observable(),
        blogPosts: ko.observableArray(),
        blogCategories: ko.observableArray()
    };

    var editableBlogViewModel = {
        blogId: ko.observable(),
        blogTitle: ko.observable(),
        blogPosts: ko.observableArray(),
        blogCategories: ko.observableArray()
    };

    var filteredPosts = ko.observableArray();
    filteredPosts.extend({ paged: { pageSize: 2 } });


    function filterByCategoryId(categoryId, event) {
        var sender = event.target;
        if ($(sender).hasClass("btn-active")) {
            pushDataToFilteredPosts();

            $(sender).removeClass("btn-active");
        } else {

            var allPosts = blogViewModel.blogPosts();
            filteredPosts.removeAll();

            for (var i = 0; i < allPosts.length; i++) {
                var currentPost = allPosts[i];
                for (var j = 0; j < currentPost.PostCategories.length; j++) {
                    var postCategory = currentPost.PostCategories[j];
                    if (postCategory.Id === categoryId) {
                        filteredPosts.push(currentPost);
                    }
                }
            };
            filteredPosts.pageNumber(1);
            var activeButtons = $(".btn-active");
            for (var i = 0; i < activeButtons.length; i++) {
                $(activeButtons[i]).removeClass("btn-active");
            }
            $(sender).addClass("btn-active");
        };

        WrapModule.ShowHidePager();
    };



    function pushDataToFilteredPosts() {
        var allPosts = blogViewModel.blogPosts();
        filteredPosts.removeAll();
        for (var i = 0; i < allPosts.length; i++) {
            filteredPosts.push(allPosts[i]);
        }

    };

    ViewModelsKO.PostViewModel = postViewModel;
    ViewModelsKO.BlogViewModel = blogViewModel;
    ViewModelsKO.EditableBlogViewModel = editableBlogViewModel;
    ViewModelsKO.FilteredPosts = filteredPosts;
    ViewModelsKO.FilterByCategoryId = filterByCategoryId;
    ViewModelsKO.PushDataToFilteredPosts = pushDataToFilteredPosts;
    

})();


