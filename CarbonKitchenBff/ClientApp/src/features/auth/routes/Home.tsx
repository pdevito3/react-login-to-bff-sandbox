
import React from 'react';
import { Link } from "react-router-dom";

function Home() {
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

