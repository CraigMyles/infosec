<?php
//db connection
try {
	$db_host = '';
	$db_dbname = '';
	$db_username = '';
	$db_password = '';

	$con = new PDO('mysql:host='.$db_host.';dbname='.$db_dbname.';charset=utf8',$db_username,$db_password);

} catch (PDOException $e) {
	$error  = "Error!: " . $e->getMessage() . "<br/>";
	die("Connection Failed");
}



//load request
$api_method = isset($_POST['api_method']) ? $_POST['api_method'] : '';
$api_data = isset($_POST['api_data']) ? $_POST['api_data'] : '';

//validate req
if (empty($api_method) || empty($api_data)){
	api_response(true, 'Invalid Request.');
}
if (!function_exists($api_method)){
	api_response(true, 'This API method is not implemented.');
}




//Find what function the client has requested

	//Do $this with $_this data.
call_user_func($api_method, $api_data);

	//Response system
function api_response($isError, $errorMessage, $responseData = '')
{
	exit(json_encode(array(
		'isError' => $isError,
		'errorMessage' => $errorMessage,
		'responseData' => $responseData
	)));
}

/*
List of required functions (x signififies existance/completion)
- USERS
	- getUsers (x)
	- addUsers (x)
	- removeUsers (x)
	- loginReq (x)
- HARDWARE
	- getCPUs (x)
	- addCPUs (x)
	- removeCPUs (x)
	- checkCPUs (x)
- QUESTIONS
	- getQuestions (x)
	- addQuestions (x)
	- removeQuestions (x)
- SCORES
	- getScores (x)
	- addScores (x)
	- resetScores (x)
*/

/*************** USER/ADMIN ACCOUNT QUERIES ***************/

function getUsers($api_data){

	try{
		global $con;
		$query = $con->prepare('SELECT users.userID, users.username FROM users WHERE isTeacher = 1');
		if($query->execute()){

			$rows = [];
			while($row = $query->fetch(PDO::FETCH_ASSOC)){
				$rows[] = $row;
			}

			api_response(false, '', $rows);
		}
		else
		{
			api_response(true, "An unknown error has occured.");
		}

	}catch(PDOException $e){
		$error = $e->getMessage();
		api_response(true, $error);
	}
}

function addUsers($api_data){
	$api_data = json_decode($api_data);
	$username = $api_data->username;
	$password = $api_data->password;

	try{
		global $con;
		$query = $con->prepare("INSERT INTO users (users.userID, users.username, users.password, users.isTeacher) VALUES (NULL, :username, :password, '1')");
		$query->execute(array(
			':username' => $username,
			':password' => $password,
		));

		if ($query) {
			api_response(false, '', '');
			//no errors, client will be made aware this was successful.
		}
		else{
			api_response(true, "An unknown error has occured.");
		}

	}catch(PDOException $e){
		$error = $e->getMessage();
		api_response(true, $error);
	}
}

function removeUsers($api_data){
	//decode login data
	$api_data = json_decode($api_data);
	$userID = $api_data->userID;
	$userID = (int)$userID;

	try{
		global $con;
		$query = $con->prepare('DELETE FROM users WHERE userID = :userID');
		
		if($query->execute(array(':userID' => $userID))){
			api_response(false, '', '');
		}
		else{
			api_response(true, "There was a problem removing this account.");
		}

	}catch(PDOException $e){
		$error = $e->getMessage();
		api_response(true, $error);
	}	
}

function userLogin($api_data){
	//decode login data
	$loginData = json_decode($api_data);
	$username = $loginData->username;
	$password = $loginData->password;

	try{
		global $con;
		$query = $con->prepare('SELECT * FROM users
			WHERE users.username = :username
			AND users.password = :password');
		$query->execute(array(
			':username' => $username,
			':password' => $password,
		));


		if ($query->rowCount() > 0) {
			api_response(false, '', 'SUCCESS');
		}
		if ($query->rowCount() == 0) {
			api_response(true, 'Username / Password does not match.');
		}
		else{
			api_response(true, "An unknown login error has occured.");
		}

	}catch(PDOException $e){
		$error = $e->getMessage();
		api_response(true, $error);
	}
}

/*************** HARDWARE/CPU QUERIES ***************/

function checkCPU($api_data){
	$api_data = json_decode($api_data);
	$processorID = $api_data->cpuID;

	try{
		global $con;
		$query = $con->prepare('SELECT * FROM verifiedHardware
			WHERE verifiedHardware.processorID = :processorID');
		$query->execute(array(
			':processorID' => $processorID,
		));

		if ($query->rowCount() > 0) {
			api_response(false, '', 'SUCCESS');
		}
		if ($query->rowCount() == 0) {
			api_response(true, 'Machine not authirised for use of this application');
		}
		else{
			api_response(true, "An unknown error has occured.");
		}

	}catch(PDOException $e){
		$error = $e->getMessage();
		api_response(true, $error);
	}
}
function listCPU($api_data){

	try{
		global $con;
		$query = $con->prepare('SELECT commonName, processorID FROM verifiedHardware');
		if($query->execute()){

			$rows = [];
			while($row = $query->fetch(PDO::FETCH_ASSOC)){
				$rows[] = $row;
			}

			api_response(false, '', $rows);
		}
		else
		{
			api_response(true, "An unknown error has occured.");
		}

	}catch(PDOException $e){
		$error = $e->getMessage();
		api_response(true, $error);
	}
}

function addCPU($api_data){
	$api_data = json_decode($api_data);
	$processorID = $api_data->processorID;
	$commonName = $api_data->commonName;

	try{
		global $con;
		$query = $con->prepare('INSERT INTO verifiedHardware (verifiedHardware.id, verifiedHardware.commonName, verifiedHardware.processorID) VALUES (NULL, :commonName, :processorID)');
		$query->execute(array(
			':commonName' => $commonName,
			':processorID' => $processorID,
		));

		if ($query) {
			api_response(false, '', '');
		}
		else{
			api_response(true, "An unknown error has occured.");
		}

	}catch(PDOException $e){
		$error = $e->getMessage();
		api_response(true, $error);
	}
}

function removeCPU($api_data){
	$api_data = json_decode($api_data);
	$commonName = $api_data->commonName;

	try{
		global $con;
		$query = $con->prepare('DELETE FROM verifiedHardware WHERE verifiedHardware.commonName = :commonName');
		$query->execute(array(
			':commonName' => $commonName,
		));

		if ($query) {
			api_response(false, '', '');
		}
		else{
			api_response(true, "An unknown error has occured.");
		}

	}catch(PDOException $e){
		$error = $e->getMessage();
		api_response(true, $error);
	}
}



/*************** QUESTION QUERIES ***************/

function getQuestions($api_data){

	try{
		global $con;
		$query = $con->prepare('SELECT questions.questionID, questions.question, questions.answerA, questions.answerB, questions.correctAnswer FROM questions');
		if($query->execute()){

			$rows = [];
			while($row = $query->fetch(PDO::FETCH_ASSOC)){
				$rows[] = $row;
			}

			api_response(false, '', $rows);
		}
		else
		{
			api_response(true, "An unknown error has occured.");
		}

	}catch(PDOException $e){
		$error = $e->getMessage();
		api_response(true, $error);
	}
}


function addQuestions($api_data){
	$api_data = json_decode($api_data);

	$question = $api_data->question;
	$answerA = $api_data->answerA;
	$answerB = $api_data->answerB;
	$correctAnswer = $api_data->correctAnswer;

	try{
		global $con;
		$query = $con->prepare('INSERT INTO questions (questions.questionID, questions.question, questions.answerA, questions.answerB, questions.correctAnswer) VALUES (NULL, :question, :answerA, :answerB, :correctAnswer)');
		if($query->execute(array(
			':question' => $question,
			':answerA' => $answerA,
			':answerB' => $answerB,
			':correctAnswer' => $correctAnswer,
		))){
			api_response(false, '', '');
		}
		else{
			api_response(true, "An unknown error has occured.");
		}

	}catch(PDOException $e){
		$error = $e->getMessage();
		api_response(true, $error);
	}
}

function removeQuestions($api_data){
	//decode login data
	$api_data = json_decode($api_data);
	$questionID = $api_data->questionID;
	$questionID = (int)$questionID;

	try{
		global $con;
		$query = $con->prepare('DELETE FROM questions WHERE questionID = :questionID');
		
		if($query->execute(array(':questionID' => $questionID))){
			api_response(false, '', '');
		}
		else{
			api_response(true, "There was a problem removing this question.");
		}

	}catch(PDOException $e){
		$error = $e->getMessage();
		api_response(true, $error);
	}	
}

/*************** SCORE QUERIES ***************/

function addScores($api_data){
	$api_data = json_decode($api_data);

	$score = $api_data->score;
	$totalQuestions = $api_data->totalQuestions;
	$username = $api_data->username;

	$scorePercentage = (100*($score/$totalQuestions));
	$now = date('Y-m-d H:i:s');

	try{
		global $con;
		$query = $con->prepare('INSERT INTO scores (scores.scoreID, scores.player, scores.percentageScore, scores.playDate) VALUES (NULL, :username, :scorePercentage, :now)');
		if($query->execute(array(
			':username' => $username,
			':scorePercentage' => $scorePercentage,
			':now' => $now,
		))){
			api_response(false, '', '');
		}
		else{
			api_response(true, "An unknown error has occured.");
		}

	}catch(PDOException $e){
		$error = $e->getMessage();
		api_response(true, $error);
	}
}
function getScores($api_data){

	try{
		global $con;
		$query = $con->prepare('SELECT scores.scoreID, scores.player, scores.percentageScore, scores.playDate FROM scores ORDER BY scores.percentageScore DESC, scores.scoreID LIMIT 10');
		if($query->execute()){

			$rows = [];
			while($row = $query->fetch(PDO::FETCH_ASSOC)){
				$rows[] = $row;
			}

			api_response(false, '', $rows);
		}
		else
		{
			api_response(true, "An unknown error has occured.");
		}

	}catch(PDOException $e){
		$error = $e->getMessage();
		api_response(true, $error);
	}
}

function resetScores($api_data){

	try{
		global $con;
		$query = $con->prepare('TRUNCATE TABLE scores');
		if($query->execute()){

			$rows = [];
			while($row = $query->fetch(PDO::FETCH_ASSOC)){
				$rows[] = $row;
			}

			api_response(false, '', '');
		}
		else
		{
			api_response(true, "An unknown error has occured.");
		}

	}catch(PDOException $e){
		$error = $e->getMessage();
		api_response(true, $error);
	}
}
?>