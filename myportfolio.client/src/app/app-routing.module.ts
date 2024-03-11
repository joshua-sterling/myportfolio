import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AboutMeComponent } from './about-me/about-me.component';
import { RowingDataComponent } from './rowing-data/rowing-data.component';

const routes: Routes = [
  { path: 'about-me', component: AboutMeComponent },
  { path: 'rowing-data', component: RowingDataComponent },
  { path: '', redirectTo: '/about-me', pathMatch: 'full' },
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
