import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
import { Post } from 'src/app/Models/Post';
import { Category } from 'src/app/Models/Category';
let PostService = class PostService {
    constructor() { }
    GetPostByID(postID) {
        let post = new Post();
        post.Id = postID;
        post.PostTitle = "new title";
        post.PostContent = "post content";
        let postCategory = new Category();
        postCategory.Id = 1;
        postCategory.CategoryName = "TestCategory";
        post.PostCategories = [postCategory];
        return post;
    }
};
PostService = tslib_1.__decorate([
    Injectable({
        providedIn: 'root'
    })
], PostService);
export { PostService };
//# sourceMappingURL=post.service.js.map