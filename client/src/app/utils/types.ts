export enum SortCriteria {
  ASC = "ASC",
  DESC = "DESC",
  NONE = "NONE",
}

export type CreateTimeRegistrationBody = {
  projectId: number;
  start: Date;
  end: Date;
};

export type GetProjectRequest = {
  projectId: number;
};

export type TimeRegistration = {
  id: number;
  start: Date;
  end: Date;
};

export type Project = {
  id: number;
  name: string;
  deadline: Date;
  timeRegistrations: TimeRegistration[];
};

export type GetProjectsRequest = {
  sortFieldName?: string;
  sortCriteria?: SortCriteria;
};

export type GetProjectsResponse = Project[];
