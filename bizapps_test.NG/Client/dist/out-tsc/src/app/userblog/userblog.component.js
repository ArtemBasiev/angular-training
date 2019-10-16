import * as tslib_1 from "tslib";
import { Component } from '@angular/core';
let UserblogComponent = class UserblogComponent {
    constructor(blogService, router, cookieService) {
        this.blogService = blogService;
        this.router = router;
        this.cookieService = cookieService;
        this.activeTabId = "blog";
        this.editModIsEnabled = false;
        this.userId = cookieService.get("userid");
    }
    ngOnInit() {
        if (this.userId != "") {
            this.blogService.GetBlogByUserID(this.userId).subscribe((data) => {
                this.userBlog = Object.assign({}, data);
                if (this.userBlog.Id == undefined || 0) {
                    this.router.navigate(['/createblog']);
                }
            });
        }
        else {
            this.router.navigate(['']);
        }
    }
    showTab(tabId) {
        this.activeTabId = tabId;
    }
    toggleEditMod() {
        this.editModIsEnabled = !this.editModIsEnabled;
    }
    updateBlog() {
        this.blogService.UpdateBlog(this.userBlog).subscribe((result) => {
            if (result) {
                this.editModIsEnabled = false;
            }
        });
    }
};
UserblogComponent = tslib_1.__decorate([
    Component({
        selector: 'app-userblog',
        templateUrl: './userblog.component.html',
        styleUrls: ['./userblog.component.css']
    })
], UserblogComponent);
export { UserblogComponent };
//# sourceMappingURL=userblog.component.js.map