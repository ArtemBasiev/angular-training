import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserblogComponent } from './userblog.component';
import { UserblogRoutingModule } from './userblog-routing.module';
import { BlogService } from '../Services/blog/blog.service';
import { FormsModule } from '@angular/forms';
import { CreateblogComponent } from './createblog/createblog.component';



@NgModule({
  declarations: [UserblogComponent, CreateblogComponent],
  imports: [
    CommonModule,
    UserblogRoutingModule,
    FormsModule
  ],
  providers: [BlogService]
})
export class UserblogModule { }
