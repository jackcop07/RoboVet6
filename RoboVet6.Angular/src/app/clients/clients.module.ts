import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ClientListComponent } from './client-list/client-list.component';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [ClientListComponent],
  imports: [
    CommonModule,
    SharedModule
  ]
})
export class ClientsModule { }
