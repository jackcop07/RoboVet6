import { Injectable } from '@angular/core';
import { IClient } from './client';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ClientService {

  private clientUrl = 'https://localhost:44387/api/clients'
  constructor(private http : HttpClient) {}

  getClients(): Observable<IClient[]>
  {
    return this.http.get<IClient[]>(this.clientUrl).pipe(
      tap(data => console.log('All: ' + JSON.stringify(data))),
      catchError(this.handleError)
    );
  }

  getProduct(id: number) : Observable<IClient>
  {
    return this.getClients().pipe(
      map(x => x.find(y => y.Id === id)),
      tap(data => console.log('Client: ' + JSON.stringify(data)))
    );
  }

  private handleError(err: HttpErrorResponse)
  {
    let errorMessage = '';
    if(err.error instanceof ErrorEvent)
    {
      errorMessage = `An error occurred: ${err.error.message}`;
    }
    else
    {
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    console.error(errorMessage);
    return throwError(errorMessage);
  }
}
