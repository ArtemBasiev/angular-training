import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
import { Post } from 'src/app/Models/Post';
let PostAdapter = class PostAdapter {
    adapt(item) {
        if (item == null)
            return null;
        var post = new Post();
        post.Id = item.Id;
        post.PostContent = item.PostContent;
        post.PostTitle = item.PostTitle;
        post.PostCategories = item.PostCategories;
        post.CreationDate = new Date(item.CreationDate);
        return post;
    }
};
PostAdapter = tslib_1.__decorate([
    Injectable({
        providedIn: 'root'
    })
], PostAdapter);
export { PostAdapter };
//# sourceMappingURL=PostAdapter.js.map