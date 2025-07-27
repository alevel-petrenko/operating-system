import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface ProcessInfo {
  id: number;
  name: string;
  threadCount: number;
  memoryUsageKb: number;
}

@Injectable({
  providedIn: 'root'
})
export class ProcessService {
  private readonly apiUrl = 'http://localhost:5147/api/process';
  
  constructor(private http: HttpClient) { }

  getProcesses() : Observable<ProcessInfo> { // 😉
    return this.http.get<ProcessInfo>(this.apiUrl);
  }
}
