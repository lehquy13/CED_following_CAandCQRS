import { Injectable } from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient, HttpParams} from "@angular/common/http";
import {map, Observable} from "rxjs";
import {PaginatedResult} from "../_models/pagination";
import {Course} from "../_models/Course";

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  baseUrl = environment.apiUrl ;
  constructor(private http : HttpClient) {}

  getCourses(page? : number, itemsPerPage? : number): Observable < PaginatedResult  <Course[]> > {
    const paginatedResult: PaginatedResult<Course[]> = new PaginatedResult<Course[]>();

    let params = new HttpParams();
    if (page != null && itemsPerPage != null) {
      params = params.append('pageIndex', page);
      params = params.append('pageSize', itemsPerPage);
    }
    return this.http.get<any>(this.baseUrl + 'ClassInformation/GetAllClassInformations', {
      observe: 'response',
      params
    }).pipe(map(response => {
      paginatedResult.result = response.body.value;
      if (response.headers.get('Pagination') != null) {
        paginatedResult.pagination = JSON.parse(response.headers.get('Pagination')!);
      }
      return paginatedResult;
    }));
  }
  getCourse(id : any): Observable < Course > {
    return this.http.get<any>(this.baseUrl + 'ClassInformation/GetClassInformation/' + id)
      .pipe(map(response => {
        return response.value;
      }
      ));
  }

}
