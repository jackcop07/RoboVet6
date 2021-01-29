import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClientListComponent } from './client-list/client-list.component';
import { SharedModule } from '../shared/shared.module';
import { RouterModule, Routes } from '@angular/router';
import { ClientSearchComponent } from './client-search/client-search.component'

const routes: Routes = [
  { path: 'clients', component: ClientListComponent }
];


@NgModule({
  declarations: [ClientListComponent, ClientSearchComponent],
  imports: [
    RouterModule.forChild(routes),
    CommonModule,
    SharedModule
  ]
})
export class ClientsModule { }
