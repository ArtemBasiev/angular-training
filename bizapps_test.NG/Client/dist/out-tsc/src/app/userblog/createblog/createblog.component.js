import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
import { Blog } from 'src/app/Models/Blog';
import { User } from 'src/app/Models/User';
let CreateblogComponent = class CreateblogComponent {
    constructor(cookieService, router, formBuilder, blogService) {
        this.cookieService = cookieService;
        this.router = router;
        this.formBuilder = formBuilder;
        this.blogService = blogService;
        this.createBlogForm = this.formBuilder.group({
            BlogTitle: ''
        });
    }
    ngOnInit() {
        let userId = this.cookieService.get("userid");
        this.blogService.GetBlogByUserID(userId).subscribe((result) => {
            if (result != null) {
                this.router.navigate(["/blog"]);
            }
        });
    }
    createBlog(blogData) {
        let blogTitle = blogData.BlogTitle;
        let userId = this.cookieService.get("userid");
        if (userId != "") {
            let blogDto = new Blog();
            blogDto.BlogTitle = blogTitle;
            let user = new User();
            user.Id = parseInt(userId, 10);
            blogDto.CreatedBy = user;
            this.blogService.CreateBlog(blogDto).subscribe((result) => {
                if (result) {
                    this.router.navigate(["/blog"]);
                }
            });
        }
        else {
            this.router.navigate([""]);
        }
    }
};
CreateblogComponent = tslib_1.__decorate([
    Component({
        selector: 'app-createblog',
        templateUrl: './createblog.component.html',
        styleUrls: ['./createblog.component.css']
    })
], CreateblogComponent);
export { CreateblogComponent };
//# sourceMappingURL=createblog.component.js.map