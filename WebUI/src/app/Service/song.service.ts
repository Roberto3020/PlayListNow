import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface Song {
  id?: number;
  title: string;
  artist: string;
  duration: number;
}

@Injectable({ providedIn: 'root' })
export class SongService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getSongs(): Observable<Song[]> {
    return this.http.get<Song[]>(this.apiUrl);
  }

  getSong(id: number): Observable<Song> {
    return this.http.get<Song>(`${this.apiUrl}/${id}`);
  }

  addSong(song: { title: string; artist: string; duration: number }): Observable<Song> {
    return this.http.post<Song>(this.apiUrl, song);
  }

  updateSong(song: Song): Observable<Song> {
    return this.http.put<Song>(`${this.apiUrl}/${song.id}`, song);
  }

  deleteSong(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
