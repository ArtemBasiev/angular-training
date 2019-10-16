import { Component, OnInit } from '@angular/core';
import { Blog } from '../Models/Blog';
import { BlogService } from '../Services/blog/blog.service';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';

@Component({
  selector: 'app-userblog',
  templateUrl: './userblog.component.html',
  styleUrls: ['./userblog.component.css']
})
export class UserblogComponent implements OnInit {

  userBlog: Blog;
  activeTabId: string = "blog";
  userId: number | string;
  editModIsEnabled: boolean = false;

  constructor(
    private blogService: BlogService,
    private router: Router,
    private cookieService: CookieService
  ) {
      this.userId = cookieService.get("userid");
   }

  ngOnInit() {

    if(this.userId!=""){
      this.blogService.GetBlogByUserID(this.userId).subscribe( (data: Blog) => {
        this.userBlog = { ...data };
  
        if(this.userBlog.Id == undefined || 0){
          this.router.navigate(['/createblog']);
         }});
    } else {
      this.router.navigate(['']);
    }
    
  }

  showTab(tabId: string){
      this.activeTabId = tabId;
  }

  toggleEditMod(){
    this.editModIsEnabled = !this.editModIsEnabled;
  }


  updateBlog(){
    this.blogService.UpdateBlog(this.userBlog).subscribe(
      (result: boolean) =>{
        if(result){
          this.editModIsEnabled = false;
        }
      }
    );
  }

}
