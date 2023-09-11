import { useQuery, UseQueryOptions } from "@tanstack/react-query";
import { convertRequestToQueryParams } from "../../utils/utils";
import { BASE_URL } from "../../utils/globals";
import { GetProjectsResponse, GetProjectsRequest } from "../../utils/types";

type UseGetProjectsOptions = Omit<
  UseQueryOptions<
    GetProjectsResponse,
    unknown,
    GetProjectsResponse,
    (string | GetProjectsRequest)[]
  >,
  "queryKey" | "queryFn"
>;

const getProjects = async (
  request: GetProjectsRequest
): Promise<GetProjectsResponse> => {
  return await fetch(
    `${BASE_URL}/projects?${convertRequestToQueryParams(request)}`
  ).then((response) => {
    if (response.ok) return response.json() as Promise<GetProjectsResponse>;
    throw Error(response.statusText);
  });
};

export default function useGetProjects(
  requestParams: GetProjectsRequest,
  options?: UseGetProjectsOptions
) {
  return useQuery(
    ["get.projects", requestParams],
    () => getProjects(requestParams),
    options
  );
}
