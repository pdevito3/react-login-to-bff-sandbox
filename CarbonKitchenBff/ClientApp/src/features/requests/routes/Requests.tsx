import React from 'react';

function Requests() {
	const people = [
		{
			name: 'Jane Cooper',
			title: 'Regional Paradigm Technician',
			department: 'Optimization',
			role: 'Admin',
			email: 'jane.cooper@example.com',
			image:
				'https://images.unsplash.com/photo-1494790108377-be9c29b29330?ixlib=rb-1.2.1&ixid=eyJhcHBfaWQiOjEyMDd9&auto=format&fit=facearea&facepad=4&w=256&h=256&q=60',
		},
	]
	return (		
		<div className="p-20">
      <div className="flex flex-col">
				<div className="-my-2 overflow-x-auto sm:-mx-6 lg:-mx-8">
					<div className="inline-block min-w-full py-2 align-middle sm:px-6 lg:px-8">
						<div className="overflow-hidden border-b border-gray-200 shadow sm:rounded-lg">
							<table className="min-w-full divide-y divide-gray-200">
								<thead className="bg-gray-50">
									<tr>
										<th scope="col" className="relative w-24 py-3">
											<span className="sr-only">Actions</span>
										</th>
										<th
											scope="col"
											className="px-6 py-3 text-xs font-medium tracking-wider text-left text-gray-500 uppercase"
										>
											Name
										</th>
										<th
											scope="col"
											className="px-6 py-3 text-xs font-medium tracking-wider text-left text-gray-500 uppercase"
										>
											Title
										</th>
										<th
											scope="col"
											className="px-6 py-3 text-xs font-medium tracking-wider text-left text-gray-500 uppercase"
										>
											Status
										</th>
										<th
											scope="col"
											className="px-6 py-3 text-xs font-medium tracking-wider text-left text-gray-500 uppercase"
										>
											Role
										</th>
									</tr>
								</thead>
								<tbody className="bg-white divide-y divide-gray-200">
									{people.map((person) => (
										<tr key={person.email}>
											<td className="px-10 py-4 text-sm text-gray-500 whitespace-nowrap">
												<svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg"><path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 12h.01M12 12h.01M19 12h.01M6 12a1 1 0 11-2 0 1 1 0 012 0zm7 0a1 1 0 11-2 0 1 1 0 012 0zm7 0a1 1 0 11-2 0 1 1 0 012 0z" /></svg>
											</td>
											<td className="px-6 py-4 whitespace-nowrap">
												<div className="flex items-center">
													<div className="flex-shrink-0 w-10 h-10">
														<img className="w-10 h-10 rounded-full" src={person.image} alt="" />
													</div>
													<div className="ml-4">
														<div className="text-sm font-medium text-gray-900">{person.name}</div>
														<div className="text-sm text-gray-500">{person.email}</div>
													</div>
												</div>
											</td>
											<td className="px-6 py-4 whitespace-nowrap">
												<div className="text-sm text-gray-900">{person.title}</div>
												<div className="text-sm text-gray-500">{person.department}</div>
											</td>
											<td className="px-6 py-4 whitespace-nowrap">
												<span className="inline-flex px-2 text-xs font-semibold leading-5 text-green-800 bg-green-100 rounded-full">
													Active
												</span>
											</td>
											<td className="px-6 py-4 text-sm text-gray-500 whitespace-nowrap">{person.role}</td>
										</tr>
									))}
								</tbody>
							</table>
						</div>
					</div>
				</div>
			</div>
    </div>
	)
}

export { Requests };

