import { Injectable } from '@angular/core';
import { HttpClient, HttpContext } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RouteConstants } from '../route.constants';

@Injectable({
  providedIn: 'root'
})
export class DataApiService {
  private readonly apiUrl = `${RouteConstants.baseUrl}api`;

  constructor(private http: HttpClient) {}

  public get<T>(endpoint: string = '', httpContext?: HttpContext): Observable<T> {
    const url = endpoint ? `${this.apiUrl}/${endpoint}` : this.apiUrl;

    return this.http.get<T>(url);
  }

  public post<T>(endoint: string, body: any, httpContext?: HttpContext): Observable<T> {
    const url = endoint ? `${this.apiUrl}/${endoint}` : this.apiUrl;

    return this.http.post<T>(url, body, { context: httpContext });
  }
}
