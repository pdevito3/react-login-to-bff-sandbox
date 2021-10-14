import React, { useRef } from 'react';
import { useForm } from "react-hook-form";
import useClaims from '../apis/claims';
import { Auth0Lock, Auth0LockPasswordless } from 'auth0-lock'
import {Link} from "react-router-dom";

function Home() {
	const { data: claimsResponse, isLoading } = useClaims();

	let claims = claimsResponse?.claims;
	// let logoutUrl = claims?.find(claim => claim.type === 'bff:logout_url')
	let nameDict = claims?.find(claim => claim.type === 'name') ||  claims?.find(claim => claim.type === 'sub');
	let username = nameDict?.value;

  const authref = useRef(null);
  console.log(authref.current)

	if(isLoading)
		return <div>Loading...</div>


	return (		
		<div className="p-20">
      <p className="text-2xl">Oncolens Portal Directory</p>
      <p className="">Please select a facility from the directory below to go to their log in page.</p>

      <div className="pt-4">
        <Link className="text-blue-300 underline hover:text-blue-400" to="/gc">Client 1</Link>
      </div>
    </div>
	)
}

export { Home };

