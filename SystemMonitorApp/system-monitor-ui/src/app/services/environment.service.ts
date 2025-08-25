import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { DataApiService } from './data-api.service';

@Injectable({
  providedIn: 'root'
})
export class EnvironmentService {
  constructor(private api: DataApiService) { }

  public getEnvironmentVariables(): Observable<Record<string, string>> {
    return this.api.get<Record<string, string>>(`environment/all`);
  }
}
