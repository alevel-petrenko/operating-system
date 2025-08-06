import { Injectable } from '@angular/core';
import { HttpClient, HttpContext } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataApiService {
  private readonly apiUrl = 'http://localhost:15708/api';

  constructor(private http: HttpClient) {}

  public get<T>(endpoint: string = '', httpContext?: HttpContext): Observable<T> {
    const url = endpoint ? `${this.apiUrl}/${endpoint}` : this.apiUrl;

    return this.http.get<T>(url);
  }

  public post(endoint: string, body: any, httpContext?: HttpContext): void {
    const url = endoint ? `${this.apiUrl}/${endoint}` : this.apiUrl;

    this.http.post(url, body, { context: httpContext });
  }
}
