import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserblogComponent } from './userblog.component';
import { CreateblogComponent } from './createblog/createblog.component';


const routes: Routes = [
  { path: 'blog', component: UserblogComponent },
  { path: 'createblog', component: CreateblogComponent }

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class UserblogRoutingModule { }