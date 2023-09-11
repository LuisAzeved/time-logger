import { useMutation } from "@tanstack/react-query";
import { BASE_URL } from "../../utils/globals";
import { CreateTimeRegistrationBody } from "../../utils/types";

const createTimeRegistration = async (body: CreateTimeRegistrationBody) => {
  return await fetch(`${BASE_URL}/timeRegistrations/create`, {
    method: "POST",
    body: JSON.stringify(body),
    headers: {
      "Content-Type": "application/json",
    },
  });
};

export default function useCreateTimeRegistration() {
  return useMutation(
    ["create-time-registration"],
    (body: CreateTimeRegistrationBody) => createTimeRegistration(body)
  );
}
