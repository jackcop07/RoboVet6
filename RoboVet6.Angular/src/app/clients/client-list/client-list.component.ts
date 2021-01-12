import { Component, OnInit } from '@angular/core';
import { IClient } from '../client';
import { ClientService } from '../client.service';

@Component({
  selector: 'app-client-list',
  templateUrl: './client-list.component.html',
  styleUrls: ['./client-list.component.css']
})
export class ClientListComponent implements OnInit {

  pageTitle: string = 'Clients';
  filteredClients: IClient[];
  clients : IClient[];
  errorMessage: string;

  _listFilter: string;
  get listFilter(): string {
    return this._listFilter;
  }

  set listFilter(value:string) {
    this._listFilter = value;
    this.filteredClients = this.listFilter ? this.performFilter(this.listFilter) : this.clients;
  }

  performFilter(filterBy: string) : IClient[] {
    filterBy = filterBy.toLowerCase();
    return this.clients.filter((client: IClient) =>
    client.LastName.toLocaleLowerCase().indexOf(filterBy) !== -1);
  }

  constructor(private clientService: ClientService) { }

  ngOnInit(): void {
    this.clientService.getClients().subscribe({
      next: clients => {
        this.clients = clients;
        this.filteredClients = this.clients;
      },
      error: err => this.errorMessage = err
    });
  }

}
