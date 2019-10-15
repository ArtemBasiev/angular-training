import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
import { Blog } from 'src/app/Models/Blog';
let BlogAdapter = class BlogAdapter {
    adapt(item) {
        if (item == null)
            return null;
        var blog = new Blog();
        blog.Id = item.Id;
        blog.BlogTitle = item.BlogTitle;
        blog.BlogCategories = item.BlogCategories;
        blog.BlogPosts = item.BlogPosts;
        blog.CreationDate = new Date(item.CreationDate);
        return blog;
    }
};
BlogAdapter = tslib_1.__decorate([
    Injectable({
        providedIn: 'root'
    })
], BlogAdapter);
export { BlogAdapter };
//# sourceMappingURL=BlogAdapter.js.map