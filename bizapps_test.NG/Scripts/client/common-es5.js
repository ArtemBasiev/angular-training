function _classCallCheck(instance, Constructor) { if (!(instance instanceof Constructor)) { throw new TypeError("Cannot call a class as a function"); } }

function _defineProperties(target, props) { for (var i = 0; i < props.length; i++) { var descriptor = props[i]; descriptor.enumerable = descriptor.enumerable || false; descriptor.configurable = true; if ("value" in descriptor) descriptor.writable = true; Object.defineProperty(target, descriptor.key, descriptor); } }

function _createClass(Constructor, protoProps, staticProps) { if (protoProps) _defineProperties(Constructor.prototype, protoProps); if (staticProps) _defineProperties(Constructor, staticProps); return Constructor; }

(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["common"], {
  /***/
  "./src/app/Adapters/BlogAdapter.ts":
  /*!*****************************************!*\
    !*** ./src/app/Adapters/BlogAdapter.ts ***!
    \*****************************************/

  /*! exports provided: BlogAdapter */

  /***/
  function srcAppAdaptersBlogAdapterTs(module, __webpack_exports__, __webpack_require__) {
    "use strict";

    __webpack_require__.r(__webpack_exports__);
    /* harmony export (binding) */


    __webpack_require__.d(__webpack_exports__, "BlogAdapter", function () {
      return BlogAdapter;
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


    var src_app_Models_Blog__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
    /*! src/app/Models/Blog */
    "./src/app/Models/Blog.ts");

    var BlogAdapter =
    /*#__PURE__*/
    function () {
      function BlogAdapter() {
        _classCallCheck(this, BlogAdapter);
      }

      _createClass(BlogAdapter, [{
        key: "adapt",
        value: function adapt(item) {
          if (item == null) return null;
          var blog = new src_app_Models_Blog__WEBPACK_IMPORTED_MODULE_2__["Blog"]();
          blog.Id = item.Id;
          blog.BlogTitle = item.BlogTitle;
          blog.BlogCategories = item.BlogCategories;
          blog.BlogPosts = item.BlogPosts;
          blog.CreationDate = new Date(item.CreationDate);
          return blog;
        }
      }]);

      return BlogAdapter;
    }();

    BlogAdapter = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])({
      providedIn: 'root'
    })], BlogAdapter);
    /***/
  },

  /***/
  "./src/app/Models/Blog.ts":
  /*!********************************!*\
    !*** ./src/app/Models/Blog.ts ***!
    \********************************/

  /*! exports provided: Blog */

  /***/
  function srcAppModelsBlogTs(module, __webpack_exports__, __webpack_require__) {
    "use strict";

    __webpack_require__.r(__webpack_exports__);
    /* harmony export (binding) */


    __webpack_require__.d(__webpack_exports__, "Blog", function () {
      return Blog;
    });
    /* harmony import */


    var tslib__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(
    /*! tslib */
    "./node_modules/tslib/tslib.es6.js");

    var Blog = function Blog() {
      _classCallCheck(this, Blog);

      this.BlogCategories = new Array();
      this.BlogPosts = new Array();
    };
    /***/

  },

  /***/
  "./src/app/Services/blog/blog.service.ts":
  /*!***********************************************!*\
    !*** ./src/app/Services/blog/blog.service.ts ***!
    \***********************************************/

  /*! exports provided: BlogService */

  /***/
  function srcAppServicesBlogBlogServiceTs(module, __webpack_exports__, __webpack_require__) {
    "use strict";

    __webpack_require__.r(__webpack_exports__);
    /* harmony export (binding) */


    __webpack_require__.d(__webpack_exports__, "BlogService", function () {
      return BlogService;
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


    var _angular_common_http__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(
    /*! @angular/common/http */
    "./node_modules/@angular/common/fesm2015/http.js");
    /* harmony import */


    var src_app_config_config_json__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(
    /*! src/app/config/config.json */
    "./src/app/config/config.json");

    var src_app_config_config_json__WEBPACK_IMPORTED_MODULE_3___namespace =
    /*#__PURE__*/
    __webpack_require__.t(
    /*! src/app/config/config.json */
    "./src/app/config/config.json", 1);
    /* harmony import */


    var rxjs_operators__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(
    /*! rxjs/operators */
    "./node_modules/rxjs/_esm2015/operators/index.js");
    /* harmony import */


    var _Adapters_BlogAdapter__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(
    /*! ../../Adapters/BlogAdapter */
    "./src/app/Adapters/BlogAdapter.ts");
    /* harmony import */


    var _auth_auth_service__WEBPACK_IMPORTED_MODULE_6__ = __webpack_require__(
    /*! ../auth/auth.service */
    "./src/app/Services/auth/auth.service.ts");

    var BlogService =
    /*#__PURE__*/
    function () {
      function BlogService(authService, http, adapter) {
        _classCallCheck(this, BlogService);

        this.authService = authService;
        this.http = http;
        this.adapter = adapter;
      }

      _createClass(BlogService, [{
        key: "GetBlogByID",
        value: function GetBlogByID(blogID) {
          var _this = this;

          var url = src_app_config_config_json__WEBPACK_IMPORTED_MODULE_3__.apiUrl + src_app_config_config_json__WEBPACK_IMPORTED_MODULE_3__.getBlogByIdUrl + blogID;
          var blog = this.http.get(url).pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (data) {
            return _this.adapter.adapt(data);
          }));
          return blog;
        }
      }, {
        key: "GetBlogByUserID",
        value: function GetBlogByUserID(userID) {
          var _this2 = this;

          var url = src_app_config_config_json__WEBPACK_IMPORTED_MODULE_3__.apiUrl + src_app_config_config_json__WEBPACK_IMPORTED_MODULE_3__.getBlogByUserIdUrl + userID;
          var blog = this.http.get(url).pipe(Object(rxjs_operators__WEBPACK_IMPORTED_MODULE_4__["map"])(function (data) {
            return _this2.adapter.adapt(data);
          }));
          return blog;
        }
      }, {
        key: "UpdateBlog",
        value: function UpdateBlog(blog) {
          var token = this.authService.GetAuthToken();
          var url = src_app_config_config_json__WEBPACK_IMPORTED_MODULE_3__.apiUrl + src_app_config_config_json__WEBPACK_IMPORTED_MODULE_3__.updateBlogUrl;
          return this.http.post(url, blog, {
            headers: new _angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpHeaders"]({
              'Content-Type': 'application/json',
              'Authorization': token
            })
          });
        }
      }, {
        key: "CreateBlog",
        value: function CreateBlog(blog) {
          var token = this.authService.GetAuthToken();
          var url = src_app_config_config_json__WEBPACK_IMPORTED_MODULE_3__.apiUrl + src_app_config_config_json__WEBPACK_IMPORTED_MODULE_3__.createBlogUrl;
          return this.http.post(url, blog, {
            headers: new _angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpHeaders"]({
              'Content-Type': 'application/json',
              'Authorization': token
            })
          });
        }
      }]);

      return BlogService;
    }();

    BlogService.ctorParameters = function () {
      return [{
        type: _auth_auth_service__WEBPACK_IMPORTED_MODULE_6__["AuthService"]
      }, {
        type: _angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpClient"]
      }, {
        type: _Adapters_BlogAdapter__WEBPACK_IMPORTED_MODULE_5__["BlogAdapter"]
      }];
    };

    BlogService = tslib__WEBPACK_IMPORTED_MODULE_0__["__decorate"]([Object(_angular_core__WEBPACK_IMPORTED_MODULE_1__["Injectable"])({
      providedIn: 'root'
    })], BlogService);
    /***/
  }
}]); //# sourceMappingURL=common-es2015.js.map
//# sourceMappingURL=common-es5.js.map