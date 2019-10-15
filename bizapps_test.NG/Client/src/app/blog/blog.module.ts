import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BlogRoutingModule } from './blog-routing.module';
import { BlogComponent } from './blog.component';
import { BlogService } from './blog.service';
import { NgxPaginationModule } from 'ngx-pagination';



@NgModule({
  declarations: [
    BlogComponent
  ],
  imports: [
    CommonModule,
    BlogRoutingModule,
    NgxPaginationModule
  ],
  providers: [BlogService]
})
export class BlogModule { }
