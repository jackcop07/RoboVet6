import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClientListComponent } from './client-list/client-list.component';
import { SharedModule } from '../shared/shared.module';
import { ClientDetailComponent } from './client-detail/client-detail.component';



@NgModule({
  declarations: [ClientListComponent, ClientDetailComponent],
  imports: [
    CommonModule,
    SharedModule
  ]
})
export class ClientsModule { }
