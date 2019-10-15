import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
import config from 'src/app/config/config.json';
import { map } from 'rxjs/operators';
let PostService = class PostService {
    constructor(http, adapter) {
        this.http = http;
        this.adapter = adapter;
    }
    GetPostByID(postID) {
        let url = config.apiUrl + config.getPostByIdUrl + postID;
        let post = this.http.get(url).pipe(map(data => this.adapter.adapt(data)));
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