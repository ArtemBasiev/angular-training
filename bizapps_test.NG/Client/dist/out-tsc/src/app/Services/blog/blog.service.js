import * as tslib_1 from "tslib";
import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';
import config from 'src/app/config/config.json';
import { map } from 'rxjs/operators';
let BlogService = class BlogService {
    constructor(authService, http, adapter) {
        this.authService = authService;
        this.http = http;
        this.adapter = adapter;
    }
    GetBlogByID(blogID) {
        let url = config.apiUrl + config.getBlogByIdUrl + blogID;
        let blog = this.http.get(url).pipe(map(data => this.adapter.adapt(data)));
        return blog;
    }
    GetBlogByUserID(userID) {
        let url = config.apiUrl + config.getBlogByUserIdUrl + userID;
        let blog = this.http.get(url).pipe(map(data => this.adapter.adapt(data)));
        return blog;
    }
    UpdateBlog(blog) {
        let token = this.authService.GetAuthToken();
        let url = config.apiUrl + config.updateBlogUrl;
        return this.http.post(url, blog, {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                'Authorization': token
            })
        });
    }
    CreateBlog(blog) {
        let token = this.authService.GetAuthToken();
        let url = config.apiUrl + config.createBlogUrl;
        return this.http.post(url, blog, {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
                'Authorization': token
            })
        });
    }
};
BlogService = tslib_1.__decorate([
    Injectable({
        providedIn: 'root'
    })
], BlogService);
export { BlogService };
//# sourceMappingURL=blog.service.js.map