import {Injectable} from '@angular/core';
import {HttpClient, HttpParams} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {map, Observable} from "rxjs";
import {PaginatedResult} from "../_models/pagination";
import {Course} from "../_models/Course";
import {Tutor} from "../_models/Tutor";

@Injectable({
  providedIn: 'root'
})
export class TutorService {
  private baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) {
  }

  getTutors(page?: number, itemsPerPage?: number, tutorParams?: any): Observable<PaginatedResult<Tutor[]>> {
    const paginatedResult: PaginatedResult<Tutor[]> = new PaginatedResult<Tutor[]>();

    let params = new HttpParams();
    if (page != null && itemsPerPage != null) {
      params = params.append('pageIndex', page);
       params = params.append('pageSize', itemsPerPage);

    }

    if (tutorParams != null) {
      params = params.append('subjectName', tutorParams.subjectName);
      params = params.append('address', tutorParams.address);
      params = params.append('gender', tutorParams.gender);
      params = params.append('birthYear', tutorParams.birthYear === null ? 0 : tutorParams.birthYear);
      params = params.append('academicLevel', tutorParams.academicLevel);
    }
console.log(params);

    return this.http.get<any>(this.baseUrl + 'TutorInformation/GetAllTutors', {
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

  getTutor(id: any): Observable<Tutor> {
    return this.http.get<Tutor>(this.baseUrl + 'TutorInformation/GetTutor/' + id)
      .pipe(map(response => {
          return response;
        }
      ));
  }
}
