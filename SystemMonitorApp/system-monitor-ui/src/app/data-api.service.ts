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
    console.log(`Fetching data from: ${url}`);
    return this.http.get<T>(url);
    // return this.http.get<T>(url, { ...(httpContext && { context: httpContext }) });
  }
}
