import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DataApiService } from '../data-api.service';
import { ProcessInfo } from '../models/ProcessInfo';

@Injectable({
  providedIn: 'root'
})
export class ProcessService {
  constructor(private api: DataApiService) { }

  public getProcesses(): Observable<ProcessInfo[]> {
    return this.api.get<ProcessInfo[]>('process/all'); ;
  }
}
