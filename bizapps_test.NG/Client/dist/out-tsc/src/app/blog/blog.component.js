import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
import { Blog } from '../Models/Blog';
let BlogComponent = class BlogComponent {
    constructor(blogService, route, router) {
        this.blogService = blogService;
        this.route = route;
        this.router = router;
        this.blog = new Blog();
        this.filteredPosts = new Array();
        this.categoryId = 0;
        this.pageNumber = 1;
        this.pageSize = 3;
        this.route.params.subscribe(param => {
            this.blogId = param['id'];
        });
    }
    ngOnInit() {
        this.blogService.GetBlogByID(this.blogId).subscribe((data) => {
            this.blog = Object.assign({}, data);
            if (this.blog.BlogPosts != undefined || null) {
                this.filteredPosts = this.blog.BlogPosts;
            }
            if (this.blog.Id == undefined || 0) {
                this.router.navigate(['/404']);
            }
        });
    }
    filterByCategory(categoryId) {
        if (this.categoryId == categoryId) {
            this.categoryId = 0;
            this.filteredPosts = this.blog.BlogPosts;
        }
        else {
            this.categoryId = categoryId;
            this.filteredPosts = this.blog.BlogPosts.filter(item => {
                let currentCategory = item.PostCategories.find(x => x.Id == categoryId);
                if (currentCategory != undefined) {
                    return item;
                }
            });
        }
    }
};
BlogComponent = tslib_1.__decorate([
    Component({
        selector: 'app-blog',
        templateUrl: './blog.component.html',
        styleUrls: ['./blog.component.css']
    })
], BlogComponent);
export { BlogComponent };
//# sourceMappingURL=blog.component.js.map