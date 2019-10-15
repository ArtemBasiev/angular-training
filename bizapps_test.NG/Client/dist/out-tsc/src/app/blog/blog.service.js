import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
import config from 'src/app/config/config.json';
import { map } from 'rxjs/operators';
let BlogService = class BlogService {
    constructor(http, adapter) {
        this.http = http;
        this.adapter = adapter;
    }
    GetBlogByID(blogID) {
        let url = config.apiUrl + config.getBlogByIdUrl + blogID;
        let blog = this.http.get(url).pipe(map(data => this.adapter.adapt(data)));
        return blog;
    }
};
BlogService = tslib_1.__decorate([
    Injectable({
        providedIn: 'root'
    })
], BlogService);
export { BlogService };
//# sourceMappingURL=blog.service.js.map