import { useQuery, UseQueryOptions } from "@tanstack/react-query";
import { BASE_URL } from "../../utils/globals";
import { GetProjectRequest, Project } from "../../utils/types";

type UseGetProjectsOptions = Omit<
  UseQueryOptions<Project, unknown, Project, (string | GetProjectRequest)[]>,
  "queryKey" | "queryFn"
>;

const getProject = async (request: GetProjectRequest): Promise<Project> => {
  return await fetch(`${BASE_URL}/projects/${request.projectId}`).then(
    (response) => {
      if (response.ok) return response.json() as Promise<Project>;
      throw Error(response.statusText);
    }
  );
};

export default function useGetProject(
  requestParams: GetProjectRequest,
  options?: UseGetProjectsOptions
) {
  return useQuery(
    ["get.project", requestParams],
    () => getProject(requestParams),
    options
  );
}
