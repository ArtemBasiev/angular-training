(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["userblog-userblog-module"],{

/***/ "./node_modules/raw-loader/dist/cjs.js!./src/app/userblog/createblog/createblog.component.html":
/*!*****************************************************************************************************!*\
  !*** ./node_modules/raw-loader/dist/cjs.js!./src/app/userblog/createblog/createblog.component.html ***!
  \*****************************************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony default export */ __webpack_exports__["default"] = ("<div id=\"divCreateBlog\" class=\"main-container\">\n    <form [formGroup]=\"createBlogForm\" (ngSubmit)=\"createBlog(createBlogForm.value)\">\n            <h4>Create your blog here</h4><br /><br />\n            <span>Blog title</span>\n            <input id=\"textBlogTitle\" name=\"BlogTitle\" type=\"text\" /><br />\n            <button class=\"buttonOnWhitePanel\" type=\"submit\">Create</button>\n            <span id=\"blogcreation-message\" style=\"color: red\"></span>\n            <span id=\"blogcreationValidationMessage\" class=\"validation-message\" >Blog Title can't be empty!</span> \n    </form>\n</div>\n");

/***/ }),

/***/ "./node_modules/raw-loader/dist/cjs.js!./src/app/userblog/userblog.component.html":
/*!****************************************************************************************!*\
  !*** ./node_modules/raw-loader/dist/cjs.js!./src/app/userblog/userblog.component.html ***!
  \****************************************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony default export */ __webpack_exports__["default"] = ("<div id=\"userblog\">\n    <div id=\"divEditBlog\" >\n        <ul class=\"nav nav-tabs\" role=\"tablist\">\n            <li [class.active]=\"activeTabId=='blog'\" class=\"tab\"><a (click)=\"showTab('blog')\">Blog info</a></li>\n            <li [class.active]=\"activeTabId=='post'\" class=\"tab\"><a (click)=\"showTab('post')\">Posts</a></li>\n            <li [class.active]=\"activeTabId=='category'\" class=\"tab\"><a (click)=\"showTab('category')\">Categories</a></li>\n        </ul>\n        <div class=\"main-container\">\n\n            <div [class.hidden]=\"activeTabId!='blog'\" id=\"blog\" class=\"div-tab\">\n                <input id=\"blogid\" type=\"hidden\" data-bind=\"value: ViewModelsKO.EditableBlogViewModel.blogId\" />\n                <span>Your blog: </span>\n                <p>\n                    <button class=\"glyph-btn\" (click)=\"toggleEditMod()\"><i class=\"glyphicon glyphicon-pencil\"></i>...</button>\n                    <span [class.hidden]=\"editModIsEnabled\" id=\"spanBlogTitle\">{{userBlog.BlogTitle}}</span>\n                    <input [class.hidden]=\"editModIsEnabled==false\" id=\"textEditableBlogTitle\" type=\"text\" style=\"width: 100px;\" [(ngModel)]=\"userBlog.BlogTitle\" />\n                    <button [class.hidden]=\"editModIsEnabled==false\" id=\"btnSaveBlog\" class=\"glyph-btn btn-save\" (click)=\"updateBlog()\">Save</button>\n                    <span id=\"blogTitleValidationMessage\" class=\"validation-message\">Blog Title can't be empty!</span>\n                </p>\n\n                <a routerLink=\"/blog/{{userBlog.Id}}\" class=\"buttonOnWhitePanel\">Review</a>\n                <button class=\"btn btn-danger\">Delete</button>\n            </div>\n\n            <div [class.hidden]=\"activeTabId!='post'\" id=\"posts\"  class=\"div-tab\">\n                <h4>Your posts:</h4>\n                <ul class=\"CategoryList\">\n                    <li *ngFor=\"let post of userBlog.BlogPosts\">\n                        <input class=\"hidden-postid\" type=\"hidden\" [value]=\"post.Id\" />\n                        <button class=\"glyph-btn\" onclick=\"PostCRUD.EnablePostEditing($(this));\" data-toggle=\"editpost\"><i class=\"glyphicon glyphicon-pencil\"></i>...</button>\n                        <button class=\"glyph-btn\" onclick=\"PostCRUD.DeletePost($(this));\"><i class=\"glyphicon glyphicon-trash\"></i></button>\n                        <span>{{post.PostTitle}}</span>\n                    </li>\n                </ul>\n                <a onclick=\"PostCRUD.EnablePostCreation($(this));\" data-toggle=\"editpost\" style=\"cursor: pointer;\">To post creation...</a>\n            </div>\n        \n            <div [class.hidden]=\"activeTabId!='category'\" id=\"categories\" style=\"display: none;\" class=\"div-tab\" >\n                <h4>Your categories:</h4>\n                <ul class=\"CategoryList\">\n                    <li *ngFor=\"let category of userBlog.BlogCategories\">\n                        <input class=\"hidden-categoryid\" type=\"hidden\" [value]=\"category.Id\" />\n                        <button class=\"glyph-btn\" onclick=\"CategoryCRUD.EnableCategoryEditing($(this));\"><i class=\"glyphicon glyphicon-pencil\"></i>...</button>\n                        <button class=\"glyph-btn\" onclick=\"CategoryCRUD.DeleteCategory($(this));\"><i class=\"glyphicon glyphicon-trash\"></i></button>\n                        <span class=\"span-categoryname\">{{category.CategoryName}}</span>\n                        <input class=\"text-categoryedit\" type=\"text\" style=\"width: 100px;\" [(ngModel)]=\"category.CategoryName\"/>\n                        <button class=\"glyph-btn btn-save\" style=\"display: none;\" onclick=\"CategoryCRUD.SaveCategoryChanges($(this));\">Save</button>\n                        <span class=\"category-validation-message validation-message\">Category Name can't be empty!</span>\n                    </li>\n                </ul><br/>\n                     <p style=\"margin-bottom: 0; padding-bottom: 5px; padding-top: 5px;\">\n                         <b> <label>Category name:</label> </b>\n                         <input id=\"textCategoryName\" type=\"text\" class=\"textboxPostTitle\" style=\"width: 250px;\" />\n                         <button id=\"ButtonCreateCategory\" onclick=\"CategoryCRUD.CreateCategory();\" class=\"buttonOnWhitePanel\">Create</button>\n                         <span id=\"spanCategoryErrorMessage\" class=\"validation-message\">Category Name can't be empty!</span>\n                     </p>\n               \n                \n            </div>\n\n        </div>\n    </div>\n\n</div>\n");

/***/ }),

/***/ "./src/app/userblog/createblog/createblog.component.css":
/*!**************************************************************!*\
  !*** ./src/app/userblog/createblog/createblog.component.css ***!
  \**************************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony default export */ __webpack_exports__["default"] = ("\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3VzZXJibG9nL2NyZWF0ZWJsb2cvY3JlYXRlYmxvZy5jb21wb25lbnQuY3NzIn0= */");

/***/ }),

/***/ "./src/app/userblog/createblog/createblog.component.ts":
/*!*************************************************************!*\
  !*** ./src/app/userblog/createblog/createblog.component.ts ***!
  \*************************************************************/
/*! exports provided: CreateblogComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "CreateblogComponent", function() { return CreateblogComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm2015/forms.js");
/* harmony import */ var ngx_cookie_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ngx-cookie-service */ "./node_modules/ngx-cookie-service/ngx-cookie-service.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm2015/router.js");
/* harmony import */ var src_app_Services_blog_blog_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! src/app/Services/blog/blog.service */ "./src/app/Services/blog/blog.service.ts");
/* harmony import */ var src_app_Models_Blog__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! src/app/Models/Blog */ "./src/app/Models/Blog.ts");
/* harmony import */ var src_app_Models_User__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! src/app/Models/User */ "./src/app/Models/User.ts");








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
            let blogDto = new src_app_Models_Blog__WEBPACK_IMPORTED_MODULE_6__["Blog"]();
            blogDto.BlogTitle = blogTitle;
            let user = new src_app_Models_User__WEBPACK_IMPORTED_MODULE_7__["User"]();
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
CreateblogComponent.ctorParameters = () => [
    { type: ngx_cookie_service__WEBPACK_IMPORTED_MODULE_3__["CookieService"] },
    { type: _angular_router__WEBPACK_IMPORTED_MODULE_4__["Router"] },
    { type: _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormBuilder"] },
    { type: src_app_Services_blog_blog_service__WEBPACK_IMPORTED_MODULE_5__["BlogService"] }
];
CreateblogComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
        selector: 'app-createblog',
        template: tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(/*! raw-loader!./createblog.component.html */ "./node_modules/raw-loader/dist/cjs.js!./src/app/userblog/createblog/createblog.component.html")).default,
        styles: [tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(/*! ./createblog.component.css */ "./src/app/userblog/createblog/createblog.component.css")).default]
    })
], CreateblogComponent);



/***/ }),

/***/ "./src/app/userblog/userblog-routing.module.ts":
/*!*****************************************************!*\
  !*** ./src/app/userblog/userblog-routing.module.ts ***!
  \*****************************************************/
/*! exports provided: UserblogRoutingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "UserblogRoutingModule", function() { return UserblogRoutingModule; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm2015/router.js");
/* harmony import */ var _userblog_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./userblog.component */ "./src/app/userblog/userblog.component.ts");
/* harmony import */ var _createblog_createblog_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./createblog/createblog.component */ "./src/app/userblog/createblog/createblog.component.ts");





const routes = [
    { path: 'blog', component: _userblog_component__WEBPACK_IMPORTED_MODULE_3__["UserblogComponent"] },
    { path: 'createblog', component: _createblog_createblog_component__WEBPACK_IMPORTED_MODULE_4__["CreateblogComponent"] }
];
let UserblogRoutingModule = class UserblogRoutingModule {
};
UserblogRoutingModule = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["NgModule"])({
        imports: [_angular_router__WEBPACK_IMPORTED_MODULE_2__["RouterModule"].forChild(routes)],
        exports: [_angular_router__WEBPACK_IMPORTED_MODULE_2__["RouterModule"]]
    })
], UserblogRoutingModule);



/***/ }),

/***/ "./src/app/userblog/userblog.component.css":
/*!*************************************************!*\
  !*** ./src/app/userblog/userblog.component.css ***!
  \*************************************************/
/*! exports provided: default */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony default export */ __webpack_exports__["default"] = ("\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3VzZXJibG9nL3VzZXJibG9nLmNvbXBvbmVudC5jc3MifQ== */");

/***/ }),

/***/ "./src/app/userblog/userblog.component.ts":
/*!************************************************!*\
  !*** ./src/app/userblog/userblog.component.ts ***!
  \************************************************/
/*! exports provided: UserblogComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "UserblogComponent", function() { return UserblogComponent; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _Services_blog_blog_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ../Services/blog/blog.service */ "./src/app/Services/blog/blog.service.ts");
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/router */ "./node_modules/@angular/router/fesm2015/router.js");
/* harmony import */ var ngx_cookie_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ngx-cookie-service */ "./node_modules/ngx-cookie-service/ngx-cookie-service.js");





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
UserblogComponent.ctorParameters = () => [
    { type: _Services_blog_blog_service__WEBPACK_IMPORTED_MODULE_2__["BlogService"] },
    { type: _angular_router__WEBPACK_IMPORTED_MODULE_3__["Router"] },
    { type: ngx_cookie_service__WEBPACK_IMPORTED_MODULE_4__["CookieService"] }
];
UserblogComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
        selector: 'app-userblog',
        template: tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(/*! raw-loader!./userblog.component.html */ "./node_modules/raw-loader/dist/cjs.js!./src/app/userblog/userblog.component.html")).default,
        styles: [tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(/*! ./userblog.component.css */ "./src/app/userblog/userblog.component.css")).default]
    })
], UserblogComponent);



/***/ }),

/***/ "./src/app/userblog/userblog.module.ts":
/*!*********************************************!*\
  !*** ./src/app/userblog/userblog.module.ts ***!
  \*********************************************/
/*! exports provided: UserblogModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "UserblogModule", function() { return UserblogModule; });
/* harmony import */ var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! tslib */ "./node_modules/tslib/tslib.es6.js");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "./node_modules/@angular/core/fesm2015/core.js");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common */ "./node_modules/@angular/common/fesm2015/common.js");
/* harmony import */ var _userblog_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./userblog.component */ "./src/app/userblog/userblog.component.ts");
/* harmony import */ var _userblog_routing_module__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! ./userblog-routing.module */ "./src/app/userblog/userblog-routing.module.ts");
/* harmony import */ var _Services_blog_blog_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! ../Services/blog/blog.service */ "./src/app/Services/blog/blog.service.ts");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(/*! @angular/forms */ "./node_modules/@angular/forms/fesm2015/forms.js");
/* harmony import */ var _createblog_createblog_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(/*! ./createblog/createblog.component */ "./src/app/userblog/createblog/createblog.component.ts");








let UserblogModule = class UserblogModule {
};
UserblogModule = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([
    Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["NgModule"])({
        declarations: [_userblog_component__WEBPACK_IMPORTED_MODULE_3__["UserblogComponent"], _createblog_createblog_component__WEBPACK_IMPORTED_MODULE_7__["CreateblogComponent"]],
        imports: [
            _angular_common__WEBPACK_IMPORTED_MODULE_2__["CommonModule"],
            _userblog_routing_module__WEBPACK_IMPORTED_MODULE_4__["UserblogRoutingModule"],
            _angular_forms__WEBPACK_IMPORTED_MODULE_6__["FormsModule"]
        ],
        providers: [_Services_blog_blog_service__WEBPACK_IMPORTED_MODULE_5__["BlogService"]]
    })
], UserblogModule);



/***/ })

}]);
//# sourceMappingURL=userblog-userblog-module-es2015.js.map