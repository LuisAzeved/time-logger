/**
 * Converts a request to a string of query params to be added after the '?' sign.
 * This stringifies and URL-encodes each top-level field of the request object.
 */
export function convertRequestToQueryParams(request: object): string {
  return (
    Object.entries(request)
      // Prevent sending empty values on the request URL.
      .filter(
        ([, value]) => value !== undefined && value !== null && value !== ""
      )
      .map(([key, value]) => `${key}=${encodeURIComponent(value)}`)
      .join("&") || ""
  );
}
