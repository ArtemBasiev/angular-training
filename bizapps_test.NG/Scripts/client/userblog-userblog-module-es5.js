function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }

function _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }

(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["userblog-userblog-module"], {
  /***/
  "./node_modules/raw-loader/dist/cjs.js!./src/app/userblog/createblog/createblog.component.html":
  /*!*****************************************************************************************************!*\
    !*** ./node_modules/raw-loader/dist/cjs.js!./src/app/userblog/createblog/createblog.component.html ***!
    \*****************************************************************************************************/

  /*! exports provided: default */

  /***/
  function node_modulesRawLoaderDistCjsJsSrcAppUserblogCreateblogCreateblogComponentHtml(module, __webpack_exports__, __webpack_require__) {
    "use strict";

    __webpack_require__.r(__webpack_exports__);
    /* harmony default export */


    __webpack_exports__["default"] = "<div id=\"divCreateBlog\" class=\"main-container\">\n    <form [formGroup]=\"createBlogForm\" (ngSubmit)=\"createBlog(createBlogForm.value)\">\n            <h4>Create your blog here</h4><br /><br />\n            <span>Blog title</span>\n            <input id=\"textBlogTitle\" name=\"BlogTitle\" type=\"text\" /><br />\n            <button class=\"buttonOnWhitePanel\" type=\"submit\">Create</button>\n            <span id=\"blogcreation-message\" style=\"color: red\"></span>\n            <span id=\"blogcreationValidationMessage\" class=\"validation-message\" >Blog Title can't be empty!</span> \n    </form>\n</div>\n";
    /***/
  },

  /***/
  "./node_modules/raw-loader/dist/cjs.js!./src/app/userblog/userblog.component.html":
  /*!****************************************************************************************!*\
    !*** ./node_modules/raw-loader/dist/cjs.js!./src/app/userblog/userblog.component.html ***!
    \****************************************************************************************/

  /*! exports provided: default */

  /***/
  function node_modulesRawLoaderDistCjsJsSrcAppUserblogUserblogComponentHtml(module, __webpack_exports__, __webpack_require__) {
    "use strict";

    __webpack_require__.r(__webpack_exports__);
    /* harmony default export */


    __webpack_exports__["default"] = "<div id=\"userblog\">\n    <div id=\"divEditBlog\" >\n        <ul class=\"nav nav-tabs\" role=\"tablist\">\n            <li [class.active]=\"activeTabId=='blog'\" class=\"tab\"><a (click)=\"showTab('blog')\">Blog info</a></li>\n            <li [class.active]=\"activeTabId=='post'\" class=\"tab\"><a (click)=\"showTab('post')\">Posts</a></li>\n            <li [class.active]=\"activeTabId=='category'\" class=\"tab\"><a (click)=\"showTab('category')\">Categories</a></li>\n        </ul>\n        <div class=\"main-container\">\n\n            <div [class.hidden]=\"activeTabId!='blog'\" id=\"blog\" class=\"div-tab\">\n                <input id=\"blogid\" type=\"hidden\" data-bind=\"value: ViewModelsKO.EditableBlogViewModel.blogId\" />\n                <span>Your blog: </span>\n                <p>\n                    <button class=\"glyph-btn\" (click)=\"toggleEditMod()\"><i class=\"glyphicon glyphicon-pencil\"></i>...</button>\n                    <span [class.hidden]=\"editModIsEnabled\" id=\"spanBlogTitle\">{{userBlog.BlogTitle}}</span>\n                    <input [class.hidden]=\"editModIsEnabled==false\" id=\"textEditableBlogTitle\" type=\"text\" style=\"width: 100px;\" [(ngModel)]=\"userBlog.BlogTitle\" />\n                    <button [class.hidden]=\"editModIsEnabled==false\" id=\"btnSaveBlog\" class=\"glyph-btn btn-save\" (click)=\"updateBlog()\">Save</button>\n                    <span id=\"blogTitleValidationMessage\" class=\"validation-message\">Blog Title can't be empty!</span>\n                </p>\n\n                <a routerLink=\"/blog/{{userBlog.Id}}\" class=\"buttonOnWhitePanel\">Review</a>\n                <button class=\"btn btn-danger\">Delete</button>\n            </div>\n\n            <div [class.hidden]=\"activeTabId!='post'\" id=\"posts\"  class=\"div-tab\">\n                <h4>Your posts:</h4>\n                <ul class=\"CategoryList\">\n                    <li *ngFor=\"let post of userBlog.BlogPosts\">\n                        <input class=\"hidden-postid\" type=\"hidden\" [value]=\"post.Id\" />\n                        <button class=\"glyph-btn\" onclick=\"PostCRUD.EnablePostEditing($(this));\" data-toggle=\"editpost\"><i class=\"glyphicon glyphicon-pencil\"></i>...</button>\n                        <button class=\"glyph-btn\" onclick=\"PostCRUD.DeletePost($(this));\"><i class=\"glyphicon glyphicon-trash\"></i></button>\n                        <span>{{post.PostTitle}}</span>\n                    </li>\n                </ul>\n                <a onclick=\"PostCRUD.EnablePostCreation($(this));\" data-toggle=\"editpost\" style=\"cursor: pointer;\">To post creation...</a>\n            </div>\n        \n            <div [class.hidden]=\"activeTabId!='category'\" id=\"categories\" style=\"display: none;\" class=\"div-tab\" >\n                <h4>Your categories:</h4>\n                <ul class=\"CategoryList\">\n                    <li *ngFor=\"let category of userBlog.BlogCategories\">\n                        <input class=\"hidden-categoryid\" type=\"hidden\" [value]=\"category.Id\" />\n                        <button class=\"glyph-btn\" onclick=\"CategoryCRUD.EnableCategoryEditing($(this));\"><i class=\"glyphicon glyphicon-pencil\"></i>...</button>\n                        <button class=\"glyph-btn\" onclick=\"CategoryCRUD.DeleteCategory($(this));\"><i class=\"glyphicon glyphicon-trash\"></i></button>\n                        <span class=\"span-categoryname\">{{category.CategoryName}}</span>\n                        <input class=\"text-categoryedit\" type=\"text\" style=\"width: 100px;\" [(ngModel)]=\"category.CategoryName\"/>\n                        <button class=\"glyph-btn btn-save\" style=\"display: none;\" onclick=\"CategoryCRUD.SaveCategoryChanges($(this));\">Save</button>\n                        <span class=\"category-validation-message validation-message\">Category Name can't be empty!</span>\n                    </li>\n                </ul><br/>\n                     <p style=\"margin-bottom: 0; padding-bottom: 5px; padding-top: 5px;\">\n                         <b> <label>Category name:</label> </b>\n                         <input id=\"textCategoryName\" type=\"text\" class=\"textboxPostTitle\" style=\"width: 250px;\" />\n                         <button id=\"ButtonCreateCategory\" onclick=\"CategoryCRUD.CreateCategory();\" class=\"buttonOnWhitePanel\">Create</button>\n                         <span id=\"spanCategoryErrorMessage\" class=\"validation-message\">Category Name can't be empty!</span>\n                     </p>\n               \n                \n            </div>\n\n        </div>\n    </div>\n\n</div>\n";
    /***/
  },

  /***/
  "./src/app/userblog/createblog/createblog.component.css":
  /*!**************************************************************!*\
    !*** ./src/app/userblog/createblog/createblog.component.css ***!
    \**************************************************************/

  /*! exports provided: default */

  /***/
  function srcAppUserblogCreateblogCreateblogComponentCss(module, __webpack_exports__, __webpack_require__) {
    "use strict";

    __webpack_require__.r(__webpack_exports__);
    /* harmony default export */


    __webpack_exports__["default"] = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3VzZXJibG9nL2NyZWF0ZWJsb2cvY3JlYXRlYmxvZy5jb21wb25lbnQuY3NzIn0= */";
    /***/
  },

  /***/
  "./src/app/userblog/createblog/createblog.component.ts":
  /*!*************************************************************!*\
    !*** ./src/app/userblog/createblog/createblog.component.ts ***!
    \*************************************************************/

  /*! exports provided: CreateblogComponent */

  /***/
  function srcAppUserblogCreateblogCreateblogComponentTs(module, __webpack_exports__, __webpack_require__) {
    "use strict";

    __webpack_require__.r(__webpack_exports__);
    /* harmony export (binding) */


    __webpack_require__.d(__webpack_exports__, "CreateblogComponent", function () {
      return CreateblogComponent;
    });
    /* harmony import */


    var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
    /*! tslib */
    "./node_modules/tslib/tslib.es6.js");
    /* harmony import */


    var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
    /*! @angular/core */
    "./node_modules/@angular/core/fesm2015/core.js");
    /* harmony import */


    var _angular_forms__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
    /*! @angular/forms */
    "./node_modules/@angular/forms/fesm2015/forms.js");
    /* harmony import */


    var ngx_cookie_service__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
    /*! ngx-cookie-service */
    "./node_modules/ngx-cookie-service/ngx-cookie-service.js");
    /* harmony import */


    var _angular_router__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
    /*! @angular/router */
    "./node_modules/@angular/router/fesm2015/router.js");
    /* harmony import */


    var src_app_Services_blog_blog_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
    /*! src/app/Services/blog/blog.service */
    "./src/app/Services/blog/blog.service.ts");
    /* harmony import */


    var src_app_Models_Blog__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(
    /*! src/app/Models/Blog */
    "./src/app/Models/Blog.ts");
    /* harmony import */


    var src_app_Models_User__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(
    /*! src/app/Models/User */
    "./src/app/Models/User.ts");

    var CreateblogComponent =
    /*#__PURE__*/
    function () {
      function CreateblogComponent(cookieService, router, formBuilder, blogService) {
        _classCallCheck(this, CreateblogComponent);

        this.cookieService = cookieService;
        this.router = router;
        this.formBuilder = formBuilder;
        this.blogService = blogService;
        this.createBlogForm = this.formBuilder.group({
          BlogTitle: ''
        });
      }

      _createClass(CreateblogComponent, [{
        key: "ngOnInit",
        value: function ngOnInit() {
          var _this = this;

          var userId = this.cookieService.get("userid");
          this.blogService.GetBlogByUserID(userId).subscribe(function (result) {
            if (result != null) {
              _this.router.navigate(["/blog"]);
            }
          });
        }
      }, {
        key: "createBlog",
        value: function createBlog(blogData) {
          var _this2 = this;

          var blogTitle = blogData.BlogTitle;
          var userId = this.cookieService.get("userid");

          if (userId != "") {
            var blogDto = new src_app_Models_Blog__WEBPACK_IMPORTED_MODULE_6__["Blog"]();
            blogDto.BlogTitle = blogTitle;
            var user = new src_app_Models_User__WEBPACK_IMPORTED_MODULE_7__["User"]();
            user.Id = parseInt(userId, 10);
            blogDto.CreatedBy = user;
            this.blogService.CreateBlog(blogDto).subscribe(function (result) {
              if (result) {
                _this2.router.navigate(["/blog"]);
              }
            });
          } else {
            this.router.navigate([""]);
          }
        }
      }]);

      return CreateblogComponent;
    }();

    CreateblogComponent.ctorParameters = function () {
      return [{
        type: ngx_cookie_service__WEBPACK_IMPORTED_MODULE_3__["CookieService"]
      }, {
        type: _angular_router__WEBPACK_IMPORTED_MODULE_4__["Router"]
      }, {
        type: _angular_forms__WEBPACK_IMPORTED_MODULE_2__["FormBuilder"]
      }, {
        type: src_app_Services_blog_blog_service__WEBPACK_IMPORTED_MODULE_5__["BlogService"]
      }];
    };

    CreateblogComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
      selector: 'app-createblog',
      template: tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(
      /*! raw-loader!./createblog.component.html */
      "./node_modules/raw-loader/dist/cjs.js!./src/app/userblog/createblog/createblog.component.html")).default,
      styles: [tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(
      /*! ./createblog.component.css */
      "./src/app/userblog/createblog/createblog.component.css")).default]
    })], CreateblogComponent);
    /***/
  },

  /***/
  "./src/app/userblog/userblog-routing.module.ts":
  /*!*****************************************************!*\
    !*** ./src/app/userblog/userblog-routing.module.ts ***!
    \*****************************************************/

  /*! exports provided: UserblogRoutingModule */

  /***/
  function srcAppUserblogUserblogRoutingModuleTs(module, __webpack_exports__, __webpack_require__) {
    "use strict";

    __webpack_require__.r(__webpack_exports__);
    /* harmony export (binding) */


    __webpack_require__.d(__webpack_exports__, "UserblogRoutingModule", function () {
      return UserblogRoutingModule;
    });
    /* harmony import */


    var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
    /*! tslib */
    "./node_modules/tslib/tslib.es6.js");
    /* harmony import */


    var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
    /*! @angular/core */
    "./node_modules/@angular/core/fesm2015/core.js");
    /* harmony import */


    var _angular_router__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
    /*! @angular/router */
    "./node_modules/@angular/router/fesm2015/router.js");
    /* harmony import */


    var _userblog_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
    /*! ./userblog.component */
    "./src/app/userblog/userblog.component.ts");
    /* harmony import */


    var _createblog_createblog_component__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
    /*! ./createblog/createblog.component */
    "./src/app/userblog/createblog/createblog.component.ts");

    var routes = [{
      path: 'blog',
      component: _userblog_component__WEBPACK_IMPORTED_MODULE_3__["UserblogComponent"]
    }, {
      path: 'createblog',
      component: _createblog_createblog_component__WEBPACK_IMPORTED_MODULE_4__["CreateblogComponent"]
    }];

    var UserblogRoutingModule = function UserblogRoutingModule() {
      _classCallCheck(this, UserblogRoutingModule);
    };

    UserblogRoutingModule = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["NgModule"])({
      imports: [_angular_router__WEBPACK_IMPORTED_MODULE_2__["RouterModule"].forChild(routes)],
      exports: [_angular_router__WEBPACK_IMPORTED_MODULE_2__["RouterModule"]]
    })], UserblogRoutingModule);
    /***/
  },

  /***/
  "./src/app/userblog/userblog.component.css":
  /*!*************************************************!*\
    !*** ./src/app/userblog/userblog.component.css ***!
    \*************************************************/

  /*! exports provided: default */

  /***/
  function srcAppUserblogUserblogComponentCss(module, __webpack_exports__, __webpack_require__) {
    "use strict";

    __webpack_require__.r(__webpack_exports__);
    /* harmony default export */


    __webpack_exports__["default"] = "\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJzcmMvYXBwL3VzZXJibG9nL3VzZXJibG9nLmNvbXBvbmVudC5jc3MifQ== */";
    /***/
  },

  /***/
  "./src/app/userblog/userblog.component.ts":
  /*!************************************************!*\
    !*** ./src/app/userblog/userblog.component.ts ***!
    \************************************************/

  /*! exports provided: UserblogComponent */

  /***/
  function srcAppUserblogUserblogComponentTs(module, __webpack_exports__, __webpack_require__) {
    "use strict";

    __webpack_require__.r(__webpack_exports__);
    /* harmony export (binding) */


    __webpack_require__.d(__webpack_exports__, "UserblogComponent", function () {
      return UserblogComponent;
    });
    /* harmony import */


    var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
    /*! tslib */
    "./node_modules/tslib/tslib.es6.js");
    /* harmony import */


    var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
    /*! @angular/core */
    "./node_modules/@angular/core/fesm2015/core.js");
    /* harmony import */


    var _Services_blog_blog_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
    /*! ../Services/blog/blog.service */
    "./src/app/Services/blog/blog.service.ts");
    /* harmony import */


    var _angular_router__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
    /*! @angular/router */
    "./node_modules/@angular/router/fesm2015/router.js");
    /* harmony import */


    var ngx_cookie_service__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
    /*! ngx-cookie-service */
    "./node_modules/ngx-cookie-service/ngx-cookie-service.js");

    var UserblogComponent =
    /*#__PURE__*/
    function () {
      function UserblogComponent(blogService, router, cookieService) {
        _classCallCheck(this, UserblogComponent);

        this.blogService = blogService;
        this.router = router;
        this.cookieService = cookieService;
        this.activeTabId = "blog";
        this.editModIsEnabled = false;
        this.userId = cookieService.get("userid");
      }

      _createClass(UserblogComponent, [{
        key: "ngOnInit",
        value: function ngOnInit() {
          var _this3 = this;

          if (this.userId != "") {
            this.blogService.GetBlogByUserID(this.userId).subscribe(function (data) {
              _this3.userBlog = Object.assign({}, data);

              if (_this3.userBlog.Id == undefined || 0) {
                _this3.router.navigate(['/createblog']);
              }
            });
          } else {
            this.router.navigate(['']);
          }
        }
      }, {
        key: "showTab",
        value: function showTab(tabId) {
          this.activeTabId = tabId;
        }
      }, {
        key: "toggleEditMod",
        value: function toggleEditMod() {
          this.editModIsEnabled = !this.editModIsEnabled;
        }
      }, {
        key: "updateBlog",
        value: function updateBlog() {
          var _this4 = this;

          this.blogService.UpdateBlog(this.userBlog).subscribe(function (result) {
            if (result) {
              _this4.editModIsEnabled = false;
            }
          });
        }
      }]);

      return UserblogComponent;
    }();

    UserblogComponent.ctorParameters = function () {
      return [{
        type: _Services_blog_blog_service__WEBPACK_IMPORTED_MODULE_2__["BlogService"]
      }, {
        type: _angular_router__WEBPACK_IMPORTED_MODULE_3__["Router"]
      }, {
        type: ngx_cookie_service__WEBPACK_IMPORTED_MODULE_4__["CookieService"]
      }];
    };

    UserblogComponent = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Component"])({
      selector: 'app-userblog',
      template: tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(
      /*! raw-loader!./userblog.component.html */
      "./node_modules/raw-loader/dist/cjs.js!./src/app/userblog/userblog.component.html")).default,
      styles: [tslib__WEBPACK_IMPORTED_MODULE_0__["__importDefault"](__webpack_require__(
      /*! ./userblog.component.css */
      "./src/app/userblog/userblog.component.css")).default]
    })], UserblogComponent);
    /***/
  },

  /***/
  "./src/app/userblog/userblog.module.ts":
  /*!*********************************************!*\
    !*** ./src/app/userblog/userblog.module.ts ***!
    \*********************************************/

  /*! exports provided: UserblogModule */

  /***/
  function srcAppUserblogUserblogModuleTs(module, __webpack_exports__, __webpack_require__) {
    "use strict";

    __webpack_require__.r(__webpack_exports__);
    /* harmony export (binding) */


    __webpack_require__.d(__webpack_exports__, "UserblogModule", function () {
      return UserblogModule;
    });
    /* harmony import */


    var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
    /*! tslib */
    "./node_modules/tslib/tslib.es6.js");
    /* harmony import */


    var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(
    /*! @angular/core */
    "./node_modules/@angular/core/fesm2015/core.js");
    /* harmony import */


    var _angular_common__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
    /*! @angular/common */
    "./node_modules/@angular/common/fesm2015/common.js");
    /* harmony import */


    var _userblog_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
    /*! ./userblog.component */
    "./src/app/userblog/userblog.component.ts");
    /* harmony import */


    var _userblog_routing_module__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
    /*! ./userblog-routing.module */
    "./src/app/userblog/userblog-routing.module.ts");
    /* harmony import */


    var _Services_blog_blog_service__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
    /*! ../Services/blog/blog.service */
    "./src/app/Services/blog/blog.service.ts");
    /* harmony import */


    var _angular_forms__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(
    /*! @angular/forms */
    "./node_modules/@angular/forms/fesm2015/forms.js");
    /* harmony import */


    var _createblog_createblog_component__WEBPACK_IMPORTED_MODULE_7__ = __webpack_require__(
    /*! ./createblog/createblog.component */
    "./src/app/userblog/createblog/createblog.component.ts");

    var UserblogModule = function UserblogModule() {
      _classCallCheck(this, UserblogModule);
    };

    UserblogModule = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["NgModule"])({
      declarations: [_userblog_component__WEBPACK_IMPORTED_MODULE_3__["UserblogComponent"], _createblog_createblog_component__WEBPACK_IMPORTED_MODULE_7__["CreateblogComponent"]],
      imports: [_angular_common__WEBPACK_IMPORTED_MODULE_2__["CommonModule"], _userblog_routing_module__WEBPACK_IMPORTED_MODULE_4__["UserblogRoutingModule"], _angular_forms__WEBPACK_IMPORTED_MODULE_6__["FormsModule"]],
      providers: [_Services_blog_blog_service__WEBPACK_IMPORTED_MODULE_5__["BlogService"]]
    })], UserblogModule);
    /***/
  }
}]); //# sourceMappingURL=userblog-userblog-module-es2015.js.map
//# sourceMappingURL=userblog-userblog-module-es5.js.map